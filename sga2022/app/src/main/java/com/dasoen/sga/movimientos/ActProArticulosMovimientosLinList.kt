package com.dasoen.sga.movimientos

import android.content.Context
import android.graphics.Bitmap
import android.graphics.BitmapFactory
import android.os.Bundle
import android.util.Base64
import android.view.View
import android.widget.ArrayAdapter
import android.widget.SearchView
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import androidx.core.view.size
import androidx.recyclerview.widget.LinearLayoutManager
import com.dasoen.sga.BuildConfig
import com.dasoen.sga.R
import com.dasoen.sga.adapterArticulo.AdapterArticulo
import com.dasoen.sga.connections.TokenAPI
import com.dasoen.sga.data.BasicData
import com.dasoen.sga.databinding.ActivityActProArticulosMovimientosLinListBinding
import com.dasoen.sga.model.Articulo
import com.squareup.okhttp.OkHttpClient
import com.squareup.okhttp.Request
import kotlinx.coroutines.*
import org.json.JSONArray


@DelicateCoroutinesApi
class ActProArticulosMovimientosLinList : AppCompatActivity() {

    private lateinit var binding: ActivityActProArticulosMovimientosLinListBinding
    private var articulosArray: MutableList<Articulo> = mutableListOf()
    private lateinit var context: Context

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityActProArticulosMovimientosLinListBinding.inflate(layoutInflater)
        setContentView(binding.root)
        context = applicationContext

        GlobalScope.async(Dispatchers.IO) {
            loadArticlesAPI()
        }

        initRecyclerView()

        ArrayAdapter.createFromResource(
            this,
            R.array.tipo_busqueda,
            android.R.layout.simple_spinner_item)
            .also { adapter ->
            adapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item)
            binding.spinnerSearch.adapter = adapter
        }

        binding.versinoApp.text = BuildConfig.VERSION_NAME
        binding.userField.text = BasicData.name
        binding.dateField.text = BasicData.date

        binding.btnBack.setOnClickListener { finish() }
        binding.searchBar.setOnQueryTextListener(object: SearchView.OnQueryTextListener{
            override fun onQueryTextSubmit(query: String): Boolean {
                return false
            }
            override fun onQueryTextChange(query: String): Boolean {
                if (binding.searchBar.size <= 0) {
                    //get()
                    initRecyclerView()
                } else {
                    binding.shimmerViewContainer.startShimmer()
                    binding.shimmerViewContainer.visibility = View.VISIBLE
                    binding.recyclerVIEW.visibility = View.GONE
                    GlobalScope.async(Dispatchers.IO){
                        searchArticleAPI(query)
                    }
                    initRecyclerView()
                }
                return false
            }
        })
    }
    private fun loadArticlesAPI() {
        if (TokenAPI.loginApi(BasicData.user, BasicData.pass)) {
            runBlocking {
                launch(Dispatchers.IO) {
                    val client = OkHttpClient()
                    val request = Request.Builder()
                        .url("http://dsoriano.ddns.net/api/Mante/TraerArticulos")
                        .addHeader("Authorization", "Bearer ${BasicData.token_access}")
                        .addHeader("Content-Type:", "application/json")
                        .build()
                    try {
                        val response = client.newCall(request).execute()
                        if (response.isSuccessful) {
                            val responseBody = response.body().string().trimIndent()
                            println(responseBody)

                            val jsonArray = JSONArray(responseBody)

                            articulosArray.clear()

                            for (i in 0 until jsonArray.length()) {

                                val art = Articulo(

                                    jsonArray.getJSONObject(i).getString("Articulo"),
                                    jsonArray.getJSONObject(i).getString("Descripcion"),
                                    jsonArray.getJSONObject(i).getString("CDB"),
                                    bitMapDecode(jsonArray.getJSONObject(i).getString("RefImg"))

                                )
                                articulosArray.add(art)
                            }
                            if (binding.shimmerViewContainer.isShimmerVisible) {
                                super.runOnUiThread {
                                    binding.shimmerViewContainer.stopShimmer()
                                    binding.shimmerViewContainer.visibility = View.GONE
                                    binding.recyclerVIEW.visibility = View.VISIBLE
                                }
                            }
                        }
                    } catch (e: Exception) {
                        e.printStackTrace()
                    }
                }
            }
        }else{
            binding.shimmerViewContainer.startShimmer()
            binding.shimmerViewContainer.visibility = View.VISIBLE
            binding.recyclerVIEW.visibility = View.GONE
            Toast.makeText(context, "No hay conexion!", Toast.LENGTH_SHORT).show()
        }
    }
    //=======================================================================================================
    //FUNCION - Pasamos un string para transformarlo en imagen, si el string es null se llena con otra imagen
    //=======================================================================================================
    private fun bitMapDecode(x: String): Bitmap {
        val bytes: ByteArray = Base64.decode(x, Base64.DEFAULT)
        return BitmapFactory.decodeByteArray(bytes,0,bytes.size)

    }
    private fun initRecyclerView(){
        binding.recyclerVIEW.layoutManager = LinearLayoutManager(this)
        binding.recyclerVIEW.adapter = AdapterArticulo(articulosArray) { itemSelected(it) }
    }
    private fun itemSelected(articulo: Articulo){
        intent.putExtra("numArticulo",articulo.numArticulo)
        intent.putExtra("desArticulo",articulo.desArticulo)
        intent.putExtra("cdbArticulo",articulo.cdbArticulo)
        setResult(RESULT_OK,intent)
        finish()
    }
    private fun searchArticleAPI(query: String) {
        if(TokenAPI.loginApi(BasicData.user,BasicData.pass)){

            var tipo = ""

            when {
                binding.spinnerSearch.selectedItem.toString() == "NumArt" -> {
                    tipo = "Articulo"
                }
                binding.spinnerSearch.selectedItem.toString() == "CDB" -> {
                    tipo = "CDB"
                }
                binding.spinnerSearch.selectedItem.toString() == "Descripcion" -> {
                    tipo = "Descripcion"
                }
            }
            val client = OkHttpClient()
            val request = Request.Builder()
                .url("http://dsoriano.ddns.net/api/Mante/TraerArticulosTipo?" +
                        "Tipo=$tipo&" +
                        "query=$query")
                .addHeader("Authorization", "Bearer ${BasicData.token_access}")
                .addHeader("Content-Type:", "application/json")
                .build()
            try {

                val response = client.newCall(request).execute()

                if (response.isSuccessful) {

                    val responseBody = response.body().string()

                    val jsonArray = JSONArray(responseBody)

                    articulosArray.clear()

                    for (i in 0 until jsonArray.length()) {

                        val art = Articulo(

                             jsonArray.getJSONObject(i).getString("Articulo"),
                            jsonArray.getJSONObject(i).getString("Descripcion"),
                            jsonArray.getJSONObject(i).getString("CDB"),
                            bitMapDecode(jsonArray.getJSONObject(i).getString("RefImg"))
                        )
                        articulosArray.add(art)
                    }
                    if (binding.shimmerViewContainer.isShimmerVisible) {
                        super.runOnUiThread {
                            binding.shimmerViewContainer.stopShimmer()
                            binding.shimmerViewContainer.visibility = View.GONE
                            binding.recyclerVIEW.visibility = View.VISIBLE
                        }
                    }
                }
            } catch (e: Exception) {
                e.printStackTrace()
            }
        }else{
            binding.shimmerViewContainer.startShimmer()
            binding.shimmerViewContainer.visibility = View.VISIBLE
            binding.recyclerVIEW.visibility = View.GONE
            Toast.makeText(context, "No hay conexion!", Toast.LENGTH_SHORT).show()
        }
    }
}
