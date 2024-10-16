package com.dasoen.sga.movimientos

import android.app.Activity
import android.content.Context
import android.content.Intent
import android.os.Bundle
import android.util.Log
import android.view.View
import android.widget.ArrayAdapter
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import com.dasoen.sga.BuildConfig
import com.dasoen.sga.connections.TokenAPI
import com.dasoen.sga.data.BasicData
import com.dasoen.sga.databinding.ActivityActProArticulosMovimientosLinBinding
import com.squareup.okhttp.OkHttpClient
import com.squareup.okhttp.Request
import kotlinx.coroutines.DelicateCoroutinesApi
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch
import kotlinx.coroutines.runBlocking
import org.json.JSONArray

@DelicateCoroutinesApi
@Suppress("DEPRECATION", "SENSELESS_COMPARISON")
class ActProArticulosMovimientosLin : AppCompatActivity() {

    private lateinit var binding: ActivityActProArticulosMovimientosLinBinding
    private lateinit var context: Context
    private lateinit var desWareHouse: String
    private lateinit var orWareHouse: String
    private lateinit var view: View
    private var client = OkHttpClient()

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityActProArticulosMovimientosLinBinding.inflate(layoutInflater)
        setContentView(binding.root)
        context = applicationContext
        view = window.decorView.rootView

        //=================================================================================
        //FUNCIONES -   Cargamos la siguiente función para llamar a la lista de alamacenes.
        //=================================================================================
        warehouseAPI()

        //========================================================================
        //NumberPicker  -   Ponemos un valor minimo de cantidad y un valor maximo.
        //========================================================================
        binding.editCantidad.maxValue = 99
        binding.editCantidad.minValue = 1

        //=============================
        //VALORES USUARIO + APP VERSION
        //=============================
        binding.versinoApp.text = BuildConfig.VERSION_NAME
        binding.userField.text = BasicData.name
        binding.dateField.text = BasicData.date

        /*
            Vamos a comprobar que tipo de Movimiento estamos tratando para que el usuario tenga mas
            facilidad a la hora de ver que almacenes puede manejar.
         */
        when (BasicData.tipoMov) {
            1 -> {
                binding.LLDestination.visibility = View.VISIBLE
            }
            2 -> {
                binding.LLOrigin.visibility = View.VISIBLE
            }
            3 -> {
                binding.LLDestination.visibility = View.VISIBLE
                binding.LLOrigin.visibility = View.VISIBLE
            }
        }

        //=======================================================================================
        //SPINNERS  -   Despues de tener en un array los alamacenes los adaptamos a los spinners.
        //=======================================================================================
        binding.alDestino.adapter = ArrayAdapter(this,android.R.layout.simple_spinner_item,BasicData.warehouseArray)
        binding.alOrigen.adapter = ArrayAdapter(this,android.R.layout.simple_spinner_item,BasicData.warehouseArray)

        //=======================================================================================
        //BOTON -   Limpiamos algunas variable y finalizariamos esta actividad llendo a la de los
        //          movimientos.
        //=======================================================================================
        binding.btnCancel.setOnClickListener{
            BasicData.idArticulo = ""
            BasicData.desArticulo = ""
            BasicData.cdbArticulo = ""
            BasicData.fechaMovTxt = ""
            finish()
        }

        //==============================================================
        //BOTON -   Para acceder a la actividad del listado de articulos
        //==============================================================
        binding.searchImage.setOnClickListener{
            Intent(context, ActProArticulosMovimientosLinList::class.java).also {
                startActivityForResult(it,1)
            }
        }

        //==============================================================
        //BOTON -   Que llama a la función "guardarArt()"
        //==============================================================
        binding.btnAceptar.setOnClickListener { //guardarArt()
            guardarArtAPI() }

        //=======================================================================================
        //CLICK EN EDITTEXT -   Hago una petición la cual es filtrada por el numero del articulo,
        //                      de ahí sacamos la descripción y la añadimos en el campo.
        //=======================================================================================
        binding.descMovimiento.setOnFocusChangeListener { _, _ ->
            if (binding.numArticulo.text.toString() != "") {
                runBlocking {
                    launch(Dispatchers.IO) {
                        if (TokenAPI.loginApi(BasicData.user, BasicData.pass)) {
                            try {
                                val request = Request.Builder()
                                    .url("http://dsoriano.ddns.net/api/Mante/TraerArticulosFiltroArticuloCBD?" +
                                            "Articulo=${binding.numArticulo.text}&" +
                                            "CDB=${binding.numArticulo.text}"
                                    )
                                    .addHeader("Authorization", "Bearer ${BasicData.token_access}")
                                    .build()
                                try {
                                    val response = client.newCall(request).execute()
                                    val responseBody = response.body().string().trimIndent()
                                    val jsonArray = JSONArray(responseBody)
                                    binding.descMovimiento.setText(jsonArray.getJSONObject(0).getString("Descripcion"))
                                } catch (e: Exception) {
                                    e.printStackTrace()
                                }

                            } catch (e: Exception) {
                                e.printStackTrace()
                            }
                        }
                    }
                }
            }
        }
    }
    //==========================================================================================
    //FUNCIÓN   -   Esta función obtiene la información del articulo seleccionado en la lista de
    //              articulos, y lo inserta en los campos.
    //==========================================================================================
    @Deprecated("Deprecated in Java")
    override fun onActivityResult(requestCode: Int, resultCode: Int, data: Intent?) {
        super.onActivityResult(requestCode, resultCode, data)
        if(requestCode == 1){
            if(resultCode == Activity.RESULT_OK){
                if (data != null) {
                    binding.numArticulo.setText(data.getStringExtra("numArticulo"))
                }
                if (data != null) {
                    binding.descMovimiento.setText(data.getStringExtra("desArticulo"))
                }
                if (data != null) {
                    binding.cdbArticulo.text = data.getStringExtra("cdbArticulo")
                }
            }
        }
    }

    //=========================================================
    //FUNCIÓN   -   Hacemos la peticion de todos los almacenes.
    //=========================================================
    private fun warehouseAPI(){
        if (TokenAPI.loginApi(BasicData.user, BasicData.pass)) {
            runBlocking {
                launch(Dispatchers.IO) {
                    try {
                        val request = Request.Builder()
                            .url("http://dsoriano.ddns.net/api/Mante/TraerAlmacenesTodos")
                            .addHeader("Authorization", "Bearer ${BasicData.token_access}")
                            .build()
                        try {
                            val response = client.newCall(request).execute()
                            if (response.isSuccessful) {
                                val responseBody = response.body().string().trimIndent()
                                val jsonArray = JSONArray(responseBody)

                                for (i in 0 until jsonArray.length()) {
                                    BasicData.warehouseArray.add(
                                        jsonArray.getJSONObject(i).getString("Almacen")
                                    )
                                }
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
    }
    private fun guardarArtAPI() {
        //Comprobamos que el numArticulo no está vacio.
        if (binding.numArticulo.text.toString() != "") {
            //Comprobamos que la descripción esté vacia.
            if (binding.descMovimiento.text.toString() != "") {
                //Dependiendo del tipo de movimiento guardaremos los valores de una forma u de otra.
                when (BasicData.tipoMov) {
                    1 -> {
                        orWareHouse = ""
                        desWareHouse = binding.alDestino.selectedItem.toString()
                    }
                    2 -> {
                        desWareHouse = ""
                        orWareHouse = binding.alOrigen.selectedItem.toString()
                    }
                    3 -> {
                        orWareHouse = binding.alOrigen.selectedItem.toString()
                        desWareHouse = binding.alDestino.selectedItem.toString()
                    }
                }
                if(TokenAPI.loginApi(BasicData.user,BasicData.pass)){
                    //Ahora, creamos un hilo para hacer el envio. (Bloqueante)
                    runBlocking {
                        launch(Dispatchers.IO) {
                            try {
                                val request = Request.Builder()
                                    .url("http://dsoriano.ddns.net/api/Mante/CrearLineasMovimientos?" +
                                            "Empresa=${BasicData.empresa}&" +
                                            "Periodo=${BasicData.year}&" +
                                            "Referencia=${BasicData.RefPadreMov}&" +
                                            "Movimiento=${BasicData.idMov}&" +
                                            "Linea=1&" +
                                            "FechaMov=${BasicData.fechaMovTxt}&" +
                                            "Cantidad=${binding.editCantidad.value}&" +
                                            "Articulo=${binding.numArticulo.text}&" +
                                            "Descripcion=${binding.descMovimiento.text}&" +
                                            "AlmacenOrigen=${orWareHouse}&" +
                                            "AlmacenDestino=${desWareHouse}&" +
                                            "AndenLogistica=&" +
                                            "EstadoLogistica=0&" +
                                            "bdcliente=&" +
                                            "CDBPedido=${binding.cdbArticulo.text}&" +
                                            "LugarDestino=0&" +
                                            "CRU=")
                                    .addHeader("Authorization", "Bearer ${BasicData.token_access}")
                                    .build()
                                try {
                                    val response = client.newCall(request).execute()
                                    if (response.isSuccessful) {
                                        val responseBody = response.body().string().trimIndent()
                                        Log.i("Info",responseBody)
                                    }
                                } catch (e: Exception) {
                                    e.printStackTrace()
                                }

                            } catch (e: Exception) {
                                e.printStackTrace()
                            }
                        }
                    }
                    //Si tod0 ha salido bien, nos avisará por pantalla con un Toast.
                    binding.numArticulo.setText("")
                    binding.descMovimiento.setText("")
                    binding.cdbArticulo.text = ""
                    Toast.makeText(applicationContext,
                        "Articulo añadido!",
                        Toast.LENGTH_SHORT)
                        .show()
                    /*Snackbar
                        .make(view,"Articulo añadido",Snackbar.LENGTH_LONG)
                        .show()*/
                }
            } else {
                //En caso de que no haya descripción nos avisará con un Toast.
                Toast.makeText(applicationContext,
                    "Escribe una breve descripcion.",
                    Toast.LENGTH_SHORT)
                    .show()

            }
        } else {
            //En caso de que no haya articulo nos avisará con un Toast.
            Toast.makeText(applicationContext,
                "Escribe o selecciona un articulo...",
                Toast.LENGTH_SHORT)
                .show()

        }
    }
}