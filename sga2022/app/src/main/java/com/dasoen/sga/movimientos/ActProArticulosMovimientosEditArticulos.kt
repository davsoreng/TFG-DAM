package com.dasoen.sga.movimientos

import android.app.Activity
import android.content.Intent
import android.graphics.Bitmap
import android.graphics.BitmapFactory
import android.os.Bundle
import android.util.Base64
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.ArrayAdapter
import android.widget.Toast
import androidx.appcompat.app.AlertDialog
import androidx.fragment.app.Fragment
import androidx.navigation.fragment.findNavController
import com.dasoen.sga.R
import com.dasoen.sga.connections.TokenAPI
import com.dasoen.sga.data.BasicData
import com.dasoen.sga.databinding.FragmentActProArticulosMovimientosEditArticulosBinding
import com.squareup.okhttp.OkHttpClient
import com.squareup.okhttp.Request
import kotlinx.coroutines.DelicateCoroutinesApi
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch
import kotlinx.coroutines.runBlocking
import org.json.JSONArray

@Suppress("DEPRECATION")
class ActProArticulosMovimientosEditArticulos : Fragment() {

    private lateinit var binding: FragmentActProArticulosMovimientosEditArticulosBinding
    private lateinit var desWareHouse: String
    private lateinit var orWareHouse: String
    private var client = OkHttpClient()

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        return FragmentActProArticulosMovimientosEditArticulosBinding
            .inflate(inflater, container, false)
            .also { binding = it }
            .root
    }

    @OptIn(DelicateCoroutinesApi::class)
    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        super.onViewCreated(view, savedInstanceState)

        binding.editCantidad.maxValue = 99
        binding.editCantidad.minValue = 1

        binding.numArticulo.isEnabled = false
        binding.descMovimiento.isEnabled = false
        binding.editCantidad.isEnabled = false
        binding.alDestino.isEnabled = false
        binding.alOrigen.isEnabled = false
        binding.searchImage.isEnabled = false

        warehouseAPI()

        binding.alDestino.adapter = ArrayAdapter(requireActivity(), R.layout.support_simple_spinner_dropdown_item,BasicData.warehouseArray)
        binding.alOrigen.adapter = ArrayAdapter(requireActivity(), R.layout.support_simple_spinner_dropdown_item,BasicData.warehouseArray)


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

        articuloListAPI()

        binding.btnVolverNormal.setOnClickListener{
            findNavController().navigate(R.id.action_actProArticulosMovimientosEditArticulos_to_actProArticulosMovimientosPreview)
        }
        binding.btnVolverEdit.setOnClickListener {
            binding.defaultButtons.visibility = View.VISIBLE
            binding.editButtons.visibility = View.GONE
            binding.numArticulo.isEnabled = false
            binding.editCantidad.isEnabled = false
            binding.descMovimiento.isEnabled = false
            binding.alOrigen.isEnabled = false
            binding.alDestino.isEnabled = false
            binding.searchImage.isEnabled = false
        }
        binding.searchImage.setOnClickListener {
            Intent(context, ActProArticulosMovimientosLinList::class.java).also {
                startActivityForResult(it,2)
            }
        }
        binding.btnEdit.setOnClickListener {
            binding.defaultButtons.visibility = View.GONE
            binding.editButtons.visibility = View.VISIBLE
            binding.numArticulo.isEnabled = true
            binding.editCantidad.isEnabled = true
            binding.descMovimiento.isEnabled = true
            binding.alOrigen.isEnabled = true
            binding.alDestino.isEnabled = true
            binding.searchImage.isEnabled = true

        }
        binding.btnGuardar.setOnClickListener {
            saveEditedArticuloAPI()
        }
        binding.btnDelete.setOnClickListener {
            deleteArticuloAPI()
        }
        binding.descMovimiento.setOnFocusChangeListener { _, _ ->
            if (binding.numArticulo.text.toString() != "") {
                if (TokenAPI.loginApi(BasicData.user, BasicData.pass)) {
                    try {
                        lateinit var jsonArray: JSONArray
                        runBlocking {
                            launch(Dispatchers.IO) {
                                val request = Request.Builder()
                                    .url("http://dsoriano.ddns.net/api/Mante/TraerArticulosFiltroArticuloCBD?" +
                                            "Articulo=${binding.numArticulo.text}&" +
                                            "CDB=${binding.numArticulo.text}"
                                    )
                                    .addHeader("Authorization", "Bearer ${BasicData.token_access}")
                                    .build()
                                try {
                                    val response = client.newCall(request).execute()
                                    val responseBody = response.body().string()
                                    jsonArray = JSONArray(responseBody)
                                } catch (e: Exception) {
                                    e.printStackTrace()
                                }
                            }
                        }
                        binding.descMovimiento.setText(jsonArray.getJSONObject(0).getString("Descripcion"))
                    } catch (e: Exception) {
                        e.printStackTrace()
                    }
                }
            }
        }
    }
    @Deprecated("Deprecated in Java")
    override fun onActivityResult(requestCode: Int, resultCode: Int, data: Intent?) {
        super.onActivityResult(requestCode, resultCode, data)
        if(requestCode == 2){
            if(resultCode == Activity.RESULT_OK){
                if (data != null) {
                    binding.numArticulo.setText(data.getStringExtra("numArticulo"))
                }
                if (data != null) {
                    binding.descMovimiento.setText(data.getStringExtra("desArticulo"))
                }
                if(TokenAPI.loginApi(BasicData.user,BasicData.pass)){
                    lateinit var jsonArray: JSONArray
                       try {
                           runBlocking {
                               launch(Dispatchers.IO) {
                                   val request = Request.Builder()
                                       .url(
                                           "http://dsoriano.ddns.net/api/Mante/TraerArticulosFiltroArticuloCBD?" +
                                                   "Articulo=${binding.numArticulo.text}&" +
                                                   "CDB=${binding.numArticulo.text}"
                                       )
                                       .addHeader(
                                           "Authorization",
                                           "Bearer ${BasicData.token_access}"
                                       )
                                       .build()
                                   try {
                                       val response = client.newCall(request).execute()
                                       if (response.isSuccessful) {
                                           val responseBody =
                                               response.body().string().trimIndent()
                                           jsonArray = JSONArray(responseBody)
                                       }
                                   } catch (e: Exception) {
                                       e.printStackTrace()
                                   }
                               }
                           }
                           for (i in 0 until jsonArray.length()) {
                               binding.imageView.setImageBitmap(
                                   bitMapDecode(jsonArray.getJSONObject(i).getString("RefImg")))
                           }
                       } catch (e: Exception) {
                           e.printStackTrace()
                       }
                }
            }
        }
    }
    private fun warehouseAPI(){
        if (TokenAPI.loginApi(BasicData.user, BasicData.pass)) {
            lateinit var jsonArray: JSONArray
               try {
                   runBlocking {
                       launch(Dispatchers.IO) {
                           val request = Request.Builder()
                               .url("http://dsoriano.ddns.net/api/Mante/TraerAlmacenesTodos")
                               .addHeader("Authorization", "Bearer ${BasicData.token_access}")
                               .build()
                           try {
                               val response = client.newCall(request).execute()
                               if (response.isSuccessful) {
                                   val responseBody = response.body().string().trimIndent()
                                   jsonArray = JSONArray(responseBody)
                               }
                           } catch (e: Exception) {
                               e.printStackTrace()
                           }
                       }
                   }
                   for (i in 0 until jsonArray.length()) {
                       BasicData.warehouseArray.add(
                           jsonArray.getJSONObject(i).getString("Almacen")
                       )
                   }
               } catch (e: Exception) {
                   e.printStackTrace()
               }
        }
    }
    private fun articuloListAPI(){
        if(TokenAPI.loginApi(BasicData.user,BasicData.pass)) {
            lateinit var jsonArray: JSONArray
            runBlocking {
                launch(Dispatchers.IO) {
                    try {
                        val request = Request.Builder()
                            .url(
                                "http://dsoriano.ddns.net/api/Mante/TraerImagenArticuloMovimientoId?" +
                                        "IdMovimiento=${BasicData.idMov}&" +
                                        "idArticulo=${BasicData.idArticuloMov}"
                            )
                            .header("Authorization", "Bearer ${BasicData.token_access}")
                            .build()
                        val response = client.newCall(request).execute()
                        val responseBody = response.body().string()
                        jsonArray = JSONArray(responseBody)
                    } catch (e: Exception) {
                        e.printStackTrace()
                    }
                }
            }
            for (i in 0 until jsonArray.length()) {

                val cantidadBuffer =
                    StringBuffer(jsonArray.getJSONObject(i).getString("Cantidad"))
                val cantidad = cantidadBuffer.delete(
                    cantidadBuffer.indexOf(","),
                    cantidadBuffer.length
                ).toString()
                binding.numArticulo.setText(
                    jsonArray.getJSONObject(i).getString("Articulo")
                )
                binding.descMovimiento.setText(
                    jsonArray.getJSONObject(i).getString("Descripcion")
                )
                binding.editCantidad.value = cantidad.toInt()
                for (j in BasicData.warehouseArray.indices) {
                    if (jsonArray.getJSONObject(i).getString("AlmacenOrigen")
                            .equals(BasicData.warehouseArray[j])) {
                        binding.alOrigen.setSelection(j)
                    }
                    if (jsonArray.getJSONObject(i).getString("AlmacenDestino")
                            .equals(BasicData.warehouseArray[j])) {
                        binding.alDestino.setSelection(j)
                    }
                }
                binding.imageView.setImageBitmap(
                    bitMapDecode(jsonArray.getJSONObject(i).getString("RefImg")))
            }
        }
    }
    private fun saveEditedArticuloAPI(){
        if (binding.numArticulo.text.toString() != "") {
            if (binding.descMovimiento.text.toString() != "") {
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
                    runBlocking {
                        launch(Dispatchers.IO) {
                            try {
                                val request = Request.Builder()
                                    .url(
                                        "http://dsoriano.ddns.net/api/Mante/ActualizarArticuloMovimiento?" +
                                                "BDCliente=&" +
                                                "AlmacenOrigen=${orWareHouse}&" +
                                                "AlmacenDestino=${desWareHouse}&" +
                                                "Articulo=${binding.numArticulo.text}&" +
                                                "Descripcion=${binding.descMovimiento.text}&" +
                                                "Lote=&" +
                                                "CRU=&" +
                                                "Cantidad=${binding.editCantidad.value}&" +
                                                "RefPadre=${BasicData.RefPadreLin}"
                                    )
                                    .header("Authorization", "Bearer ${BasicData.token_access}")
                                    .build()
                                val response = client.newCall(request).execute()
                                val responseBody = response.body().string().trimIndent()
                                Log.i("INFO",responseBody)
                                requireActivity().runOnUiThread {
                                    Toast.makeText(context,"Articulo editado exitosamente!", Toast.LENGTH_SHORT)
                                        .show()
                                    Thread.sleep(1000L)
                                    findNavController()
                                        .navigate(R.id.action_actProArticulosMovimientosEditArticulos_to_actProArticulosMovimientosPreview)
                                }

                            } catch (e: Exception) {
                                e.printStackTrace()
                            }
                        }
                    }
                }
                binding.defaultButtons.visibility = View.VISIBLE
                binding.editButtons.visibility = View.GONE
                binding.numArticulo.isEnabled = false
                binding.editCantidad.isEnabled = false
                binding.descMovimiento.isEnabled = false
                binding.alOrigen.isEnabled = false
                binding.alDestino.isEnabled = false
                binding.searchImage.isEnabled = false
            }else{
                Toast.makeText(requireContext(),
                    "Escribe una breve descripcion.",
                    Toast.LENGTH_SHORT)
                    .show()
            }
        }else{
            Toast.makeText(requireContext(),
                "Escribe o selecciona un articulo...",
                Toast.LENGTH_SHORT)
                .show()
        }
    }
    //======================================================================================================
    //FUNCIÓN - Esta función
    //=======================================================================================================
    private fun deleteArticuloAPI(){
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
            val builder = AlertDialog.Builder(requireActivity())
            builder.setTitle("ELIMINAR ARTICULO")
            builder.setMessage("Estas borrando el articulo: ${binding.numArticulo.text}, con ${binding.descMovimiento.text}, " +
                    "\ndesde el almacen de origen: $orWareHouse hasta el almacen destino $desWareHouse, " +
                    "\ncon la cantidad de: ${binding.editCantidad.value}." +
                    "\n ¿Desea borrarlo?")
            builder.setPositiveButton("SI") { _, _ ->
                if(TokenAPI.loginApi(BasicData.user,BasicData.pass)) {
                    runBlocking {
                        launch(Dispatchers.IO) {
                            try {
                                val request = Request.Builder()
                                    .url(
                                        "http://dsoriano.ddns.net/api/Mante/EliminarArticuloMovimiento?" +
                                                "Empresa=${BasicData.empresa}&" +
                                                "BDCliente=&" +
                                                "Movimiento=${BasicData.idMov}&" +
                                                "RefPadre=${BasicData.RefPadreLin}"
                                    )
                                    .header("Authorization", "Bearer ${BasicData.token_access}")
                                    .build()
                                val response = client.newCall(request).execute()
                                val responseBody = response.body().string().trimIndent()
                                Log.i("INFO", responseBody)
                                requireActivity().runOnUiThread {
                                    Toast.makeText(context,"Articulo Eliminado!", Toast.LENGTH_SHORT)
                                        .show()
                                    Thread.sleep(1000L)
                                    findNavController()
                                        .navigate(R.id.action_actProArticulosMovimientosEditArticulos_to_actProArticulosMovimientosPreview)
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
    //=======================================================================================================
    //FUNCION - Pasamos un string para transformarlo en imagen, si el string es null se llena con otra imagen
    //=======================================================================================================
    private fun bitMapDecode(x: String): Bitmap {
        val bytes: ByteArray = Base64.decode(x, Base64.DEFAULT)
        return if(BitmapFactory.decodeByteArray(bytes,0,bytes.size)==null){
            BitmapFactory.decodeResource(resources,R.drawable.logo_sga_2022)
        }else{
            BitmapFactory.decodeByteArray(bytes,0,bytes.size)
        }
    }
}
