package com.dasoen.sga.menu

import android.annotation.SuppressLint
import android.content.Context
import android.content.Intent
import android.icu.text.SimpleDateFormat
import android.os.Bundle
import android.view.View
import android.widget.ArrayAdapter
import android.widget.SearchView
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import androidx.core.view.size
import androidx.recyclerview.widget.LinearLayoutManager
import com.dasoen.sga.ActLogin
import com.dasoen.sga.BuildConfig
import com.dasoen.sga.R
import com.dasoen.sga.adapterMovimiento.AdapterMovimiento
import com.dasoen.sga.connections.TokenAPI
import com.dasoen.sga.data.BasicData
import com.dasoen.sga.databinding.ActivityActMenuMovimientosBinding
import com.dasoen.sga.model.Movimiento
import com.dasoen.sga.movimientos.ActProArticulosMovimientos
import com.dasoen.sga.movimientos.ActProArticulosMovimientosInfo
import com.squareup.okhttp.OkHttpClient
import com.squareup.okhttp.Request
import kotlinx.coroutines.*
import org.json.JSONArray

@DelicateCoroutinesApi
class ActMenuMovimientos : AppCompatActivity() {

    private lateinit var binding: ActivityActMenuMovimientosBinding
    private lateinit var context: Context
    private var movimientosArray: MutableList<Movimiento> = mutableListOf()
    @SuppressLint("SimpleDateFormat")
    private var simpleDateFormat = SimpleDateFormat("dd/MM/yyyy")

    @SuppressLint("SimpleDateFormat")

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityActMenuMovimientosBinding.inflate(layoutInflater)
        setContentView(binding.root)
        context = applicationContext

        GlobalScope.async(Dispatchers.IO) {
            traerMovimientosAPI()
        }

        initRecyclerView()

        //=======================================================================
        //SPINNER - AÑADIMOS DESDE UN ARCHIVO UN ARRAY, Y LO ADAPTAMOS AL SPINNER
        //=======================================================================
        ArrayAdapter.createFromResource(context, R.array.movimiento_busqueda, android.R.layout.simple_spinner_item)
            .also { adapter -> adapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item)
                binding.spinnerMov.adapter = adapter }

        //=============================
        //VALORES USUARIO + APP VERSION
        //=============================
        binding.versinoApp.text = BuildConfig.VERSION_NAME
        binding.userField.text = BasicData.name
        binding.dateField.text = BasicData.date

        //======================================
        //BOTON - Volver a la actividad anterior
        //======================================
        binding.btnBack.setOnClickListener {
            Intent(context, ActLogin::class.java).also {
                startActivity(it)
                BasicData.cleanData()
                finish()
            }
        }

        //=================================
        //BOTON - Crear un nuevo movimiento
        //=================================
        binding.btnNuevo.setOnClickListener {
            Intent(context, ActProArticulosMovimientos::class.java).also {
                startActivity(it)
            }
        }

        //======================================
        //BOTON(BUSCADOR) - FILTRARÁ CUANDO LE DEMOS A LA LUPA CUANDO LE DEMOS A LA LUPA.
        //======================================
        binding.MovSearch.setOnQueryTextListener(object : SearchView.OnQueryTextListener {
            override fun onQueryTextSubmit(query: String): Boolean {
                return false
            }

            override fun onQueryTextChange(query: String): Boolean {
                if (binding.MovSearch.size <= 0) {
                    //get()
                    initRecyclerView()
                } else {
                    GlobalScope.async(Dispatchers.IO) {
                        searchMovsAPI(query)
                    }
                    initRecyclerView()
                }
                return false
            }
        })

        //===================================================
        //SwipeRefreshLayout - Refrescará nustro recyclerview
        //===================================================
        binding.refreshMov.setOnRefreshListener {
            //get()
            GlobalScope.async(Dispatchers.IO) {
                traerMovimientosAPI()
            }
            initRecyclerView()
            binding.refreshMov.isRefreshing = false
        }
    }

    //===================================================
    //RecyclerAdapter - Adapatará nuetra lista al recyclerview
    //===================================================
    private fun initRecyclerView(){
        binding.MovRecycler.layoutManager = LinearLayoutManager(context)
        binding.MovRecycler.adapter = AdapterMovimiento(movimientosArray) { itemSelected(it) }
    }

    //===================================================
    //RecyclerView - Al seleccionar el movimiento, guardará varias variable,
    //                  además tambien nos llevará a otra atividad.
    //===================================================
    private fun itemSelected(movimiento: Movimiento){
        BasicData.idMov = movimiento.idOrigen
        BasicData.RefPadreMov = movimiento.RefPadre
        BasicData.tipoMov = movimiento.tipoMov
        BasicData.Cerrado = movimiento.servido
        Intent(context,ActProArticulosMovimientosInfo::class.java).also {
            startActivity(it)
        }
    }

    //============================================================
    //FUNCION - Con esta función traemos los movimiento.
    //============================================================
    private fun traerMovimientosAPI(){
        if(TokenAPI.loginApi(BasicData.user,BasicData.pass)) {
            val client = OkHttpClient()
            val request = Request.Builder()
                .url("http://dsoriano.ddns.net/api/Mante/TraerMovimientos?anyo=2022")
                .addHeader("Authorization", "Bearer ${BasicData.token_access}")
                .addHeader("Content-Type:", "application/json")
                .build()
            try {

                val response = client.newCall(request).execute()

                if (response.isSuccessful) {

                    val responseBody = response.body().string().trimIndent()

                    val jsonArray = JSONArray(responseBody)

                    movimientosArray.clear()

                    for (i in 0 until jsonArray.length()) {

                        val mov = Movimiento(
                            jsonArray.getJSONObject(i).getInt("MovimientoOrigen"),
                            jsonArray.getJSONObject(i).getString("RefPadre"),
                            jsonArray.getJSONObject(i).getInt("TipoMovimiento"),
                            simpleDateFormat.parse(jsonArray.getJSONObject(i).getString("Fecha")),
                            jsonArray.getJSONObject(i).getString("Hora"),
                            jsonArray.getJSONObject(i).getString("Descripcion"),
                            jsonArray.getJSONObject(i).getInt("MovimientoONLine"),
                            jsonArray.getJSONObject(i).getInt("Servido")
                        )

                        movimientosArray.add(mov)
                    }
                    if (binding.shimmerViewContainer.isShimmerVisible) {
                        super.runOnUiThread {
                            binding.shimmerViewContainer.stopShimmer()
                            binding.shimmerViewContainer.visibility = View.GONE
                            binding.refreshMov.visibility = View.VISIBLE
                        }
                    }
                }
            } catch (e: Exception) {
                e.printStackTrace()
            }
        }else{
            super.runOnUiThread {
                binding.shimmerViewContainer.startShimmer()
                binding.shimmerViewContainer.visibility = View.VISIBLE
                binding.refreshMov.visibility = View.GONE
                Toast.makeText(context, "No hay conexion!", Toast.LENGTH_SHORT).show()
            }
        }
    }
    //=======================================================================================================
    //FUNCION - Con esta función traemos los movimiento según por que tipo y la cadena escrita por el usuario
    //=======================================================================================================
    private fun searchMovsAPI(query: String){
        var tipo = ""
        when {
            binding.spinnerMov.selectedItem.toString() == "ID MOVIMIENTO" -> {
                tipo = "MovimientoOrigen"
            }
            binding.spinnerMov.selectedItem.toString() == "DESCRIPCION" -> {
                tipo = "Observaciones"
            }
            binding.spinnerMov.selectedItem.toString() == "ID ONLINE" -> {
                tipo = "MovimientoONLine"
            }
        }
           if(TokenAPI.loginApi(BasicData.user,BasicData.pass)){
               val client = OkHttpClient()
               val request = Request.Builder()
                   .url("http://dsoriano.ddns.net/api/Mante/TraerMovimientosFiltro?" +
                           "anyo=${BasicData.year}&" +
                           "tipo=$tipo&" +
                           "query=$query")
                   .addHeader("Authorization","Bearer ${BasicData.token_access}")
                   .addHeader("Content-Type:","application/json")
                   .build()
               try {

                   val response = client.newCall(request).execute()

                   if(response.isSuccessful) {

                       val responseBody = response.body().string().trimIndent()

                       val jsonArray = JSONArray(responseBody)

                       movimientosArray.clear()

                       for (i in 0 until jsonArray.length()) {
                           val mov = Movimiento(
                               jsonArray.getJSONObject(i).getInt("MovimientoOrigen"),
                               jsonArray.getJSONObject(i).getString("RefPadre"),
                               jsonArray.getJSONObject(i).getInt("TipoMovimiento"),
                               simpleDateFormat.parse(jsonArray.getJSONObject(i).getString("Fecha")),
                               jsonArray.getJSONObject(i).getString("Hora"),
                               jsonArray.getJSONObject(i).getString("Descripcion"),
                               jsonArray.getJSONObject(i).getInt("MovimientoONLine"),
                               jsonArray.getJSONObject(i).getInt("Servido")
                           )
                           movimientosArray.add(mov)
                       }
                       if(binding.shimmerViewContainer.isShimmerVisible) {
                           super.runOnUiThread{
                               binding.shimmerViewContainer.stopShimmer()
                               binding.shimmerViewContainer.visibility = View.GONE
                               binding.refreshMov.visibility = View.VISIBLE
                           }
                       }
                   }
               }catch (e: Exception){
                   e.printStackTrace()
               }
           }else {
               super.runOnUiThread {
                   binding.shimmerViewContainer.startShimmer()
                   binding.shimmerViewContainer.visibility = View.VISIBLE
                   binding.refreshMov.visibility = View.GONE
                   Toast.makeText(context, "No hay conexion!", Toast.LENGTH_SHORT).show()
               }
           }
    }
}