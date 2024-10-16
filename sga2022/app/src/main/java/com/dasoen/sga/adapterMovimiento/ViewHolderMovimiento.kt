package com.dasoen.sga.adapterMovimiento

import android.annotation.SuppressLint
import android.view.View
import androidx.recyclerview.widget.RecyclerView
import com.dasoen.sga.databinding.ItemMovimientoBinding
import com.dasoen.sga.model.Movimiento
import java.text.SimpleDateFormat

class ViewHolderMovimiento(view: View): RecyclerView.ViewHolder(view) {

    private val binding = ItemMovimientoBinding.bind(view)

    @SuppressLint("CheckResult", "SimpleDateFormat")
    fun render(movimientoModel: Movimiento, OnClickListener:(Movimiento)-> Unit){
        binding.idMovView.text = movimientoModel.idOrigen.toString()
        binding.refPadreMovView.text = movimientoModel.RefPadre
        binding.tipoMovView.text = movimientoModel.tipoMov.toString()
        binding.fechaMovView.text = SimpleDateFormat("dd-MM-yyyy").format(movimientoModel.fechaMov).toString()
        binding.timeMovView.text = movimientoModel.timeMov
        binding.descripcionMovView.text = movimientoModel.descripcionMov
        binding.idOnlineMovView.text = movimientoModel.idOnline.toString()
        val cerrado = movimientoModel.servido
        binding.switchCerrado.isChecked = cerrado == 1
        itemView.setOnClickListener{OnClickListener(movimientoModel)}
    }
}