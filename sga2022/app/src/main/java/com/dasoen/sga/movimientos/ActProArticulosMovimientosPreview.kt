package com.dasoen.sga.movimientos

import android.annotation.SuppressLint
import android.content.Context
import android.content.Intent
import android.graphics.Bitmap
import android.graphics.BitmapFactory
import android.icu.text.SimpleDateFormat
import android.os.Bundle
import android.util.Base64
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Toast
import androidx.appcompat.app.AlertDialog
import androidx.fragment.app.Fragment
import androidx.navigation.fragment.findNavController
import androidx.recyclerview.widget.LinearLayoutManager
import com.dasoen.sga.R
import com.dasoen.sga.adapterArticuloMov.AdapterArticuloMov
import com.dasoen.sga.connections.TokenAPI
import com.dasoen.sga.data.BasicData
import com.dasoen.sga.databinding.FragmentActProArticulosMovimientosPreviewBinding
import com.dasoen.sga.menu.ActMenuMovimientos
import com.dasoen.sga.model.ArticuloMov
import com.squareup.okhttp.OkHttpClient
import com.squareup.okhttp.Request
import kotlinx.coroutines.*
import org.json.JSONArray


@DelicateCoroutinesApi
class ActProArticulosMovimientosPreview : Fragment() {

    private lateinit var binding: FragmentActProArticulosMovimientosPreviewBinding
    private var articulosmovimientosArray: MutableList<ArticuloMov> = mutableListOf()
    @SuppressLint("SimpleDateFormat")
    private var simpleDateFormat = SimpleDateFormat("dd/MM/yyyy")

    override fun onCreateView(inflater: LayoutInflater, container: ViewGroup?, savedInstanceState: Bundle?): View {
        return FragmentActProArticulosMovimientosPreviewBinding
            .inflate(inflater, container, false)
            .also { binding = it }
            .root
    }

    @Suppress("DEPRECATION")
    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        super.onViewCreated(view, savedInstanceState)

        /*
            Comprobamos si la variable "x" del movimiento es igual a uno, en ese caso los botones,
            se desactivarán.
         */

        if(BasicData.Cerrado == 1){
            binding.btnEdit.isEnabled = false
            binding.btDeleteMov.isEnabled = false
            binding.addArticulo.isEnabled = false
        }

        //Dentro de un hilo no blocante, hacemos la llamada a la función "movPreviewAPI()" y a "articuloListAPI()"
        GlobalScope.async(Dispatchers.IO) {
            movPreviewAPI()
        }
        GlobalScope.async(Dispatchers.IO) {
            articuloListAPI()
        }
        //Cargamos el resultado de la lista de los articulos del movimiento.
        initRecyclerView()


        //=======================================================
        //BOTON -   Llamará a la función y le pasara la actividad
        //=======================================================
        binding.btDeleteMov.setOnClickListener{
            //deleteMov(requireActivity())
            deleteMovAPI(requireActivity())
        }

        //=================================================================================
        //BOTON -   Nos llevará a la actividad de los movimientos y destruirá el fragmento.
        //=================================================================================
        binding.btnBack.setOnClickListener{
            Intent(requireActivity(), ActMenuMovimientos::class.java).also {
                startActivity(it)
                BasicData.idMov = 0
                BasicData.RefPadreMov = ""
                BasicData.tipoMov = 0
                BasicData.Cerrado = 0
            }
        }

        //==========================================================================
        //BOTON -   Nos llevará a un nuevo fragemento utilizando el navigation_view.
        //==========================================================================
        binding.btnEdit.setOnClickListener{
            findNavController().navigate(R.id.action_actProArticulosMovimientosPreview_to_actProArticulosMovimientosEdit)
        }

        //=========================================================
        //BOTON -   Abrirá la actividad con los articulos de la BD.
        //=========================================================
        binding.addArticulo.setOnClickListener{
            Intent(context, ActProArticulosMovimientosLin::class.java).also {
                startActivity(it)
            }
        }

        //==============================================================================
        //RefresArticulosMov -   Actualizará el listado de los arituclos del movimiento.
        //==============================================================================
        binding.refreshMov.setOnRefreshListener {
            GlobalScope.async(Dispatchers.IO) {
                movPreviewAPI()
            }
            GlobalScope.async(Dispatchers.IO) {
                articuloListAPI()
            }
            //Cargamos el resultado de la lista de los articulos del movimiento.
            initRecyclerView()
            binding.refreshMov.isRefreshing = false
        }
    }
    //=================================================================================================
    //FUNCION   -   En esta función vamos a hacer una peticion a la BD, en ella le vamos a pasar el ID,
    //              ya que lo tenemos de la selección del mismo en el menú de movimientos.
    //=================================================================================================
    private fun movPreviewAPI(){
        if(TokenAPI.loginApi(BasicData.user,BasicData.pass)) {
           val client = OkHttpClient()
           val request = Request.Builder()
               .url("http://dsoriano.ddns.net/api/Mante/TraerMovimientoId?" +
                       "IdMovimiento=${BasicData.idMov}")
               .addHeader("Authorization", "Bearer ${BasicData.token_access}")
               .addHeader("Content-Type:", "application/json")
               .build()
           try {

               val response = client.newCall(request).execute()

               if (response.isSuccessful) {

                   val responseBody = response.body().string().trimIndent()

                   val jsonArray = JSONArray(responseBody)


                   for (i in 0 until jsonArray.length()) {
                       requireActivity().runOnUiThread {
                           BasicData.idMov =
                               jsonArray.getJSONObject(i).getInt("MovimientoOrigen")
                           binding.idMovView.text = BasicData.idMov.toString()
                           BasicData.RefPadreMov =
                               jsonArray.getJSONObject(i).getString("RefPadre")
                           binding.refPadreMovView.text = BasicData.RefPadreMov
                           binding.tipoMovView.text =
                               jsonArray.getJSONObject(i).getInt("TipoMovimiento").toString()
                           BasicData.fechaMovTxt =
                               jsonArray.getJSONObject(i).getString("Fecha")
                           binding.fechaMovView.text = BasicData.fechaMovTxt
                           binding.timeMovView.text =
                               jsonArray.getJSONObject(i).getString("Hora")
                           binding.descripcionMovView.text =
                               jsonArray.getJSONObject(i).getString("Descripcion")
                           binding.idOnlineMovView.text =
                               jsonArray.getJSONObject(i).getInt("MovimientoONLine").toString()
                           val cerrado =  jsonArray.getJSONObject(i).getInt("Servido")
                           //Comprobaremos si el movimiento está servidor, en caso de que esté nos aparecerá como checkeado.
                           if(cerrado == 1){binding.switchCerrado.isChecked = true}
                           if(cerrado == 0){binding.switchCerrado.isChecked = false}
                       }
                   }
               }
           } catch (e: Exception) {
               e.printStackTrace()
           }
        }else{
            Toast.makeText(context,"No hay conexion!",Toast.LENGTH_SHORT).show()
        }
    }

    //=====================================================================
    //FUNCION   -   Cargaremos en esta función los articulos de Movimiento.
    //=====================================================================
    private fun articuloListAPI(){
        if(TokenAPI.loginApi(BasicData.user,BasicData.pass)) {
            val client = OkHttpClient()
            val request = Request.Builder()
                .url("http://dsoriano.ddns.net/api/Mante/TraerArticulosMovimientoId?" +
                        "IdMovimiento=${BasicData.idMov}")
                .addHeader("Authorization", "Bearer ${BasicData.token_access}")
                .addHeader("Content-Type:", "application/json")
                .build()
            try {

                val response = client.newCall(request).execute()

                if (response.isSuccessful) {

                    val responseBody = response.body().string().trim()

                    val jsonArray = JSONArray(responseBody)

                    //LIMPIAMOS LA LISTA
                    articulosmovimientosArray.clear()

                    for (i in 0 until jsonArray.length()) {

                        //COMO LA BD DEVUELVE UN VALOR CON COMA, se debe tratar para que sea entero.
                        val cantidadBuffer = StringBuffer(jsonArray.getJSONObject(i).getString("Cantidad"))
                        val cantidad = cantidadBuffer.delete(cantidadBuffer.indexOf(","),cantidadBuffer.length).toString()

                        val arti = ArticuloMov(
                            jsonArray.getJSONObject(i).getString("Articulo"),
                            jsonArray.getJSONObject(i).getString("Descripcion"),
                            cantidad.toInt(),
                            jsonArray.getJSONObject(i).getString("AlmacenDestino"),
                            jsonArray.getJSONObject(i).getString("AlmacenOrigen"),
                            jsonArray.getJSONObject(i).getString("RefPadre"),
                            bitMapDecode(jsonArray.getJSONObject(i).getString("RefImg"))
                        )
                        /*
                Posiblemente el movimiento puede tener + de 1 articulo por ello se alamacena en una lista,
                y posteriormente se inserta en un Recyclerview.
             */
                        articulosmovimientosArray.add(arti)
                    }
                    if(articulosmovimientosArray.isEmpty()){
                        requireActivity().runOnUiThread {
                            binding.shimmerViewContainer.stopShimmer()
                            binding.shimmerViewContainer.visibility = View.GONE
                            binding.noArticulos.visibility = View.VISIBLE
                        }

                    }else{
                        binding.noArticulos.visibility = View.GONE
                        if (binding.shimmerViewContainer.isShimmerVisible) {
                            requireActivity().runOnUiThread {
                                binding.shimmerViewContainer.stopShimmer()
                                binding.shimmerViewContainer.visibility = View.GONE
                                binding.refreshMov.visibility = View.VISIBLE
                            }
                        }
                    }

                }
            } catch (e: Exception) {
                e.printStackTrace()
            }
        }else{
            binding.shimmerViewContainer.startShimmer()
            binding.shimmerViewContainer.visibility = View.VISIBLE
            binding.refreshMov.visibility = View.GONE
            Toast.makeText(context, "No hay conexion!", Toast.LENGTH_SHORT).show()
        }
    }

    //=======================================================================================================
    //FUNCION - Pasamos un string para transformarlo en imagen, si el string es null se llena con otra imagen
    //=======================================================================================================
    private fun bitMapDecode(x: String): Bitmap {
        val bytes: ByteArray = Base64.decode(x, Base64.DEFAULT)
        if(BitmapFactory.decodeByteArray(bytes,0,bytes.size)==null){
            return BitmapFactory.decodeResource(resources,R.drawable.logo_sga_2022)
        }else{
            return BitmapFactory.decodeByteArray(bytes,0,bytes.size)
        }
    }

    //================================================
    //FUNCION - Adapatará nuetra lista al recyclerview
    //================================================
    private fun initRecyclerView(){
       binding.ArticulosMovRecycler.layoutManager = LinearLayoutManager(context)
       binding.ArticulosMovRecycler.adapter = AdapterArticuloMov(articulosmovimientosArray) { itemSelected(it) }
    }

    //===================================================
    //FUNCION - al seleccionar nuestro articulo, comprobaremos si el
    //===================================================
    private fun itemSelected(ArticuloMov: ArticuloMov){
        if (BasicData.Cerrado == 0){
            BasicData.idArticuloMov = ArticuloMov.idArticulo
            BasicData.descArticuloMov = ArticuloMov.desArticulo
            BasicData.RefPadreLin = ArticuloMov.referPadreArticulo
            findNavController().navigate(R.id.action_actProArticulosMovimientosPreview_to_actProArticulosMovimientosEditArticulos)
        }
    }
    private fun deleteMovAPI(context: Context){
        val builder = AlertDialog.Builder(requireActivity())
        builder.setTitle("¿ELIMINAR MOVIMIENTO?")
        builder.setMessage("Estas borrando el movimiento: ${binding.idMovView.text} de tipo: ${binding.tipoMovView.text}, " +
                "\ncreado el ${binding.fechaMovView.text} a las ${binding.timeMovView.text}")
        builder.setPositiveButton("SI") { _, _ ->
            if(TokenAPI.loginApi(BasicData.user,BasicData.pass)){
                runBlocking {
                    launch(Dispatchers.IO) {
                        val client = OkHttpClient()
                        val request = Request.Builder()
                            .url("http://dsoriano.ddns.net/api/Mante/EliminarMovimiento?" +
                                    "Empresa=${BasicData.empresa}&" +
                                    "bdcliente=&" +
                                    "RefPadre=${BasicData.RefPadreMov}")
                            .addHeader("Authorization", "Bearer ${BasicData.token_access}")
                            .addHeader("Content-Type:", "application/json")
                            .build()
                        try {
                            val response = client.newCall(request).execute()
                            if (response.isSuccessful) {
                                val responseBody = response.body().string().trimIndent()
                                Log.i("info",responseBody)
                                Thread.sleep(750L)
                                requireActivity().runOnUiThread {
                                    Toast.makeText(context,"Movimiento Eliminado!", Toast.LENGTH_SHORT)
                                        .show()
                                }
                                BasicData.idMov = 0
                                BasicData.idMovOnline = 0
                                BasicData.RefPadreMov = ""
                                BasicData.tipoMov = 0
                                BasicData.desMovimiento = ""
                                BasicData.Cerrado = 0
                                BasicData.fechaMovTxt = ""
                                Thread.sleep(500L)
                                requireActivity().finish()

                            }
                        } catch (e: Exception) {
                            e.printStackTrace()
                        }
                    }
                }
            }
        }
        builder.setNegativeButton("NO"){_,_ -> }
        builder.show()
    }
}