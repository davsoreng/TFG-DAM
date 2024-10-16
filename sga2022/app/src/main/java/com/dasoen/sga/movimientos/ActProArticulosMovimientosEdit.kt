package com.dasoen.sga.movimientos

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import androidx.navigation.fragment.findNavController
import com.dasoen.sga.R
import com.dasoen.sga.connections.TokenAPI
import com.dasoen.sga.data.BasicData
import com.dasoen.sga.databinding.FragmentActProArticulosMovimientosEditBinding
import com.dasoen.sga.model.ArticuloMov
import com.squareup.okhttp.OkHttpClient
import com.squareup.okhttp.Request
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch
import kotlinx.coroutines.runBlocking
import org.json.JSONArray
import java.sql.Connection
import java.sql.ResultSet
import java.sql.Statement

class ActProArticulosMovimientosEdit : Fragment() {

    private lateinit var binding: FragmentActProArticulosMovimientosEditBinding
    private var articulosmovimientosArray: MutableList<ArticuloMov> = mutableListOf()
    private var client = OkHttpClient()

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        return FragmentActProArticulosMovimientosEditBinding
            .inflate(inflater, container, false)
            .also { binding = it }
            .root
    }

    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        super.onViewCreated(view, savedInstanceState)

        //CARGAMOS LA FUNCIÓN DEL MOVIMIENTO
        movPreviewAPI()

        binding.btnGuardar.setOnClickListener{
            saveEditAPI()
        }
        binding.btnBack.setOnClickListener{
            findNavController().navigate(R.id.action_actProArticulosMovimientosEdit_to_actProArticulosMovimientosPreview)
        }
    }
    //=======================================================================================================
    //FUNCIÓN - Esta función carga los datos del movimiento, y añade al editext la descripción para poder
    // ser editada.
    //=======================================================================================================
    private fun movPreviewAPI() {
        if(TokenAPI.loginApi(BasicData.user,BasicData.pass)){
            lateinit var jsonArray:JSONArray
            try {
                runBlocking {
                    launch(Dispatchers.IO) {
                        val request = Request.Builder()
                            .url(
                                "http://dsoriano.ddns.net/api/Mante/TraerMovimientoId?" +
                                        "IdMovimiento=${BasicData.idMov}")
                            .addHeader(
                                "Authorization", "Bearer ${BasicData.token_access}")
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
                    binding.idMovView.text =
                        jsonArray.getJSONObject(i).getString("MovimientoOrigen")
                    binding.tipoMovView.text =
                        jsonArray.getJSONObject(i).getString("TipoMovimiento")
                    binding.fechaMovView.text = BasicData.fechaMovTxt
                    binding.timeMovView.text = jsonArray.getJSONObject(i).getString("Hora")
                    binding.descripcionMovView.setText(
                        jsonArray.getJSONObject(i).getString("Descripcion")
                    )
                    binding.idOnlineMovView.text =
                        jsonArray.getJSONObject(i).getString("MovimientoONLine")
                    binding.refPadreMovView.text = jsonArray.getJSONObject(i).getString("RefPadre")
                }
            }catch (e: Exception){
                e.printStackTrace()
            }
        }
    }
    //=======================================================================================================
    //FUNCIÓN - Cuando se quiera guardar se llamará a esta función teniendo en cuenta los cambios que se
    // han realizado tanto en la descripción como en el estado servido.
    //=======================================================================================================
    private fun saveEditAPI(){
        if(TokenAPI.loginApi(BasicData.user,BasicData.pass)){
            try{
                runBlocking {
                    launch(Dispatchers.IO) {
                        when {
                            binding.switchCerrado.isChecked ->{
                                BasicData.Cerrado = 1
                            }
                            !binding.switchCerrado.isChecked ->{
                                BasicData.Cerrado = 0
                            }
                        }
                        val request = Request.Builder()
                            .url(
                                "http://dsoriano.ddns.net/api/Mante/ActualizarMovimiento?" +
                                        "dbcliente=&" +
                                        "Descripcion=${binding.descripcionMovView.text.toString()}&" +
                                        "RefPadre=${BasicData.RefPadreMov}&" +
                                        "Servido=${BasicData.Cerrado}"
                            )
                            .addHeader(
                                "Authorization", "Bearer ${BasicData.token_access}"
                            )
                            .build()
                        try {
                            val response = client.newCall(request).execute()
                            if (response.isSuccessful) {
                                requireActivity().runOnUiThread {
                                    findNavController()
                                        .navigate(R.id.action_actProArticulosMovimientosEdit_to_actProArticulosMovimientosPreview)
                                }
                            }
                        } catch (e: Exception) {
                            e.printStackTrace()
                        }
                    }
                }
            }catch (e: Exception){
                e.printStackTrace()
            }
        }
        BasicData.desMovimiento = binding.descripcionMovView.text.toString()
    }
}