package com.dasoen.sga.movimientos

import android.content.Context
import android.content.Intent
import android.os.Bundle
import android.util.Log
import android.widget.ArrayAdapter
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import com.dasoen.sga.BuildConfig
import com.dasoen.sga.R
import com.dasoen.sga.connections.TokenAPI
import com.dasoen.sga.data.BasicData
import com.dasoen.sga.databinding.ActivityActProArticulosMovimientosBinding
import com.squareup.okhttp.OkHttpClient
import com.squareup.okhttp.Request
import kotlinx.coroutines.DelicateCoroutinesApi
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch
import kotlinx.coroutines.runBlocking
import org.json.JSONArray
import java.util.*


@DelicateCoroutinesApi
@Suppress("DEPRECATION", "NULLABILITY_MISMATCH_BASED_ON_JAVA_ANNOTATIONS")
class ActProArticulosMovimientos : AppCompatActivity() {

    private lateinit var binding: ActivityActProArticulosMovimientosBinding
    private lateinit var context: Context
    private var client = OkHttpClient()

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityActProArticulosMovimientosBinding.inflate(layoutInflater)
        setContentView(binding.root)
        context = applicationContext

        //=======================================================================
        //SPINNER - AÑADIMOS DESDE UN ARCHIVO UN ARRAY, Y LO ADAPTAMOS AL SPINNER
        //=======================================================================
        ArrayAdapter.createFromResource(this, R.array.tipo_movimientos,android.R.layout.simple_spinner_item)
            .also { adapter -> adapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item)
            binding.spinnerMov.adapter = adapter }

        //=============================
        //VALORES USUARIO + APP VERSION
        //=============================
        binding.versinoApp.text = BuildConfig.VERSION_NAME
        binding.userField.text = BasicData.name
        binding.dateField.text = BasicData.date

        //===============================================================
        //BOTON FINALIZAR ACTIVIDAD + BOTON LLAMAR FUNCION "guardarArt()"
        //===============================================================
        binding.btnCancel.setOnClickListener{ finish() }
        binding.btnAceptar.setOnClickListener{
            //guardarArtTCP()
            guardarArtAPI()
        }
    }
    //=======================================================================================================
    //FUNCION - Esta función comprueba la descripción no está vacia, en caso de que este bién,
    //          se realizará una petición la cual devolverá el contador de los movimientos
    //          y por ultimo guardará el movimiento en la base de datos.
    //=======================================================================================================
    private fun guardarArtAPI(){
        //Extraemos la fecha del datepicker.
        try {
            BasicData.fechaMovTxt =
                binding.datePicker.dayOfMonth
                    .toString() + "-" + (binding.datePicker.month + 1)
                    .toString() + "-" + binding.datePicker.year
                    .toString()
            if (binding.descMovimiento.text.toString() != "") {
                if (binding.datePicker.year >= Date().year) {
                    when {
                        binding.spinnerMov.selectedItem.toString() == "Entrada" -> {
                            BasicData.tipoMov = 1
                        }
                        binding.spinnerMov.selectedItem.toString() == "Salida" -> {
                            BasicData.tipoMov = 2
                        }
                        binding.spinnerMov.selectedItem.toString() == "Intercambio" -> {
                            BasicData.tipoMov = 3
                        }
                    }
                    if (TokenAPI.loginApi(BasicData.user, BasicData.pass)) {
                        runBlocking {
                            launch(Dispatchers.IO) {
                                try {
                                    val request = Request.Builder()
                                        .url("http://dsoriano.ddns.net/api/Mante/TraerContadorMovimientos?" +
                                                    "Empresa=${BasicData.empresa}&" +
                                                    "Periodo=${BasicData.year}&" +
                                                    "Serie=&" +
                                                    "Formulario=frmProArticulosMovimiento&" +
                                                    "Tabla=ProArticulosMovimiento&" +
                                                    "CampoAContar=ValorActual"
                                        )
                                        .addHeader(
                                            "Authorization", "Bearer ${BasicData.token_access}"
                                        )
                                        .build()
                                    try {
                                        val response = client.newCall(request).execute()
                                        if (response.isSuccessful) {
                                            val responseBody = response.body().string().trimIndent()
                                            val jsonArray = JSONArray(responseBody)
                                            BasicData.idMov =
                                                jsonArray.getJSONObject(0).getString("Column1")
                                                    .toInt()
                                        }
                                    } catch (e: Exception) {
                                        e.printStackTrace()
                                    }

                                } catch (e: Exception) {
                                    e.printStackTrace()
                                }
                            }
                        }
                    }
                    if (TokenAPI.loginApi(BasicData.user, BasicData.pass)) {
                        runBlocking {
                            launch(Dispatchers.IO) {
                                try {
                                    val request = Request.Builder()
                                        .url(
                                            "http://dsoriano.ddns.net/api/Mante/CrearCabeceraMovimientos?" +
                                                    "Empresa=${BasicData.empresa}&" +
                                                    "Periodo=${BasicData.year}&" +
                                                    "Movimiento=${BasicData.idMov}&" +
                                                    "FechaMov=${BasicData.fechaMovTxt}&" +
                                                    "Usuario=${BasicData.user}&" +
                                                    "bdcliente=empty&" +
                                                    "AndenLogistica=empty&" +
                                                    "OrigenLogistica=empty&" +
                                                    "Observaciones=${binding.descMovimiento.text.toString()}&" +
                                                    "EstadoLogistica=0&" +
                                                    "TipoMovimiento=${BasicData.tipoMov}&" +
                                                    "OrigenMovimiento=1&" +
                                                    "LugarDestino=empty&" +
                                                    "AlmacenOrigen=empty&" +
                                                    "AlmacenDestino=empty&" +
                                                    "Servido=0"
                                        )
                                        .header("Authorization", "Bearer ${BasicData.token_access}")
                                        .build()
                                    try {
                                        val response = client.newCall(request).execute()
                                        try {
                                            val responseBody =
                                                response.body().string().trimIndent()
                                            Log.i("INFO",responseBody)
                                            val jsonArray = JSONArray(responseBody)

                                            Log.i("INFO",responseBody)
                                            BasicData.RefPadreMov =
                                                jsonArray.getJSONObject(0).getString("RefPadre")
                                            Log.i("INFO","===========================================")
                                            Log.i("INFO",BasicData.RefPadreMov)
                                            Log.i("INFO","===========================================")
                                            /*
                                            Si tod0 a ido bien y no ha habido ningún error, nos avisará por pantalla,
                                            con un Toast que el movimeinto ha sido creado correctament.
                                        */
                                            super.runOnUiThread {
                                                Toast.makeText(
                                                    applicationContext,
                                                    "Movimiento creado!",
                                                    Toast.LENGTH_SHORT
                                                )
                                                    .show()
                                                Thread.sleep(1500)
                                            }
                                            /*
                                            Despues de avisarnos de que se ha creado, nos llevará a otra Actividad.
                                         */
                                            Intent(
                                                applicationContext,
                                                ActProArticulosMovimientosLin::class.java
                                            ).also {
                                                startActivity(it)
                                                finish()
                                            }
                                        } catch (e: Exception) {
                                            e.printStackTrace()
                                        }


                                    } catch (e: Exception) {
                                        e.printStackTrace()
                                    }

                                } catch (e: Exception) {
                                    e.printStackTrace()
                                }
                            }
                        }
                    }
                } else {
                    Toast.makeText(
                        applicationContext,
                        "El año no debe ser menor al actual.",
                        Toast.LENGTH_SHORT
                    )
                        .show()

                }
            } else {
                Toast.makeText(
                    applicationContext,
                    "Escribe una breve descripcion.",
                    Toast.LENGTH_SHORT
                )
                    .show()
            }
        }catch (e: Exception){
            e.printStackTrace()
        }
    }
}