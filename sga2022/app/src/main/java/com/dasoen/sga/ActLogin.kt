package com.dasoen.sga

import android.Manifest.permission.READ_EXTERNAL_STORAGE
import android.Manifest.permission.WRITE_EXTERNAL_STORAGE
import android.annotation.SuppressLint
import android.content.Context
import android.content.Intent
import android.content.pm.PackageManager
import android.os.Build
import android.os.Bundle
import android.util.Log
import android.widget.Toast
import androidx.annotation.RequiresApi
import androidx.appcompat.app.AppCompatActivity
import androidx.core.app.ActivityCompat
import androidx.core.content.ContextCompat
import com.dasoen.sga.connections.TokenAPI
import com.dasoen.sga.data.BasicData
import com.dasoen.sga.databinding.ActivityMainBinding
import com.dasoen.sga.menu.ActMenuMovimientos
import com.squareup.okhttp.OkHttpClient
import com.squareup.okhttp.Request
import kotlinx.coroutines.DelicateCoroutinesApi
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch
import kotlinx.coroutines.runBlocking
import org.json.JSONArray
import java.sql.Connection
import java.sql.ResultSet
import java.sql.Statement

@DelicateCoroutinesApi
@Suppress("DEPRECATION", "DEPRECATED_IDENTITY_EQUALS")
class ActLogin : AppCompatActivity() {

    private lateinit var binding: ActivityMainBinding
    private lateinit var sentGen: Statement
    private lateinit var context: Context
    private lateinit var connGen: Connection
    private lateinit var rs: ResultSet
    private var result: String = ""
    private lateinit var name:String
    private lateinit var pass:String

    @RequiresApi(Build.VERSION_CODES.R)
    override fun onCreate(savedInstanceState: Bundle?) {

        Thread.sleep(1000L)

        setTheme(R.style.Theme_ProjectLogin)

        //===================
        //PERMISOS - ARCHIVOS
        //===================
        if (ContextCompat.checkSelfPermission
                (this, READ_EXTERNAL_STORAGE) != PackageManager.PERMISSION_GRANTED
        ) {
            ActivityCompat.requestPermissions(this, arrayOf(READ_EXTERNAL_STORAGE), 1)
        }
        if (ContextCompat.checkSelfPermission
                (this, WRITE_EXTERNAL_STORAGE) != PackageManager.PERMISSION_GRANTED
        ) {
            ActivityCompat.requestPermissions(this, arrayOf(WRITE_EXTERNAL_STORAGE), 1)
        }

        super.onCreate(savedInstanceState)
        binding = ActivityMainBinding.inflate(layoutInflater)
        setContentView(binding.root)

        context = applicationContext

        //VERSION APP
        binding.versinoApp.text = BuildConfig.VERSION_NAME
        binding.dateField.text = BasicData.date

        binding.userText.setText("")
        binding.userPass.setText("")

        //==============================================
        //BOTON - INICIO DE SESION + COMPROBACION CAMPOS
        //==============================================
        binding.logInBtn.setOnClickListener {
            //signUser(context)
            if (binding.userText.text.toString() != "") {
                name = binding.userText.text.toString()
                if (binding.userPass.text.toString() != "") {
                    pass = binding.userPass.text.toString()
                    //logIn(name,pass)
                    //loginApi()
                    log()
                } else {
                    binding.textInputPassword.error =
                        "Rellena el campo"
                }
            } else {
                binding.textInputUsername.error =
                    "Rellena el campo"
            }
        }

        //==========================
        //TEXTFIELD - LIMPIAR CAMPOS
        //==========================
        binding.userText.setOnClickListener {
            binding.userText.setText("")
        }
        binding.userPass.setOnClickListener {
            binding.userPass.setText("")
        }
    }
    //=======================
    //LOG IN - API REST
    //=======================
    @SuppressLint("RestrictedApi")
    private fun log(){
        if(TokenAPI.loginApi(binding.userText.text.toString(),binding.userPass.text.toString())){
            BasicData.user = binding.userText.text.toString()
            BasicData.pass = binding.userPass.text.toString()
            runBlocking {
                launch(Dispatchers.IO) {
                    if(TokenAPI.loginApi(BasicData.user,BasicData.pass)){
                        val client = OkHttpClient()
                        val request = Request.Builder()
                            .url("http://dsoriano.ddns.net/api/Mante/TraerInfoUsuario?" +
                                    "NombrePersona=${BasicData.user}&Contrasenya=${BasicData.pass}")
                            .addHeader("Authorization","Bearer ${BasicData.token_access}")
                            .addHeader("Content-Type:","application/json")
                            .build()

                        try {
                            val response = client.newCall(request).execute()
                            if(response.isSuccessful) {
                                val responseBody = response.body().string()
                                Log.i("INFO",responseBody.toString())
                                val jsonArray = JSONArray(responseBody)
                                for (i in 0 until jsonArray.length()) {
                                    BasicData.empresa = jsonArray.getJSONObject(i).getString("Empresa")
                                    BasicData.NumUsuario = jsonArray.getJSONObject(i).getString("Usuario")
                                    BasicData.RefPadreUser = jsonArray.getJSONObject(i).getString("RefPadre")
                                    BasicData.name = jsonArray.getJSONObject(i).getString("NombrePersona")
                                }
                                super.runOnUiThread{
                                    Toast.makeText(context,"Credenciales Correctos!",Toast.LENGTH_SHORT).show()
                                    Thread.sleep(500L)
                                }
                                Intent(context,ActMenuMovimientos::class.java).also {
                                    startActivity(it)
                                }
                            }
                        }catch (e: Exception){
                            e.printStackTrace()
                        }
                    }
                }
            }
        }else{
            Toast.makeText(context,"Error en la autenticación, comprueba tus credenciales",Toast.LENGTH_SHORT).show()
        }
    }
}
