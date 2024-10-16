package com.dasoen.sga.adapterArticulo

import android.annotation.SuppressLint
import android.view.View
import androidx.recyclerview.widget.RecyclerView
import com.dasoen.sga.databinding.ItemArticuloBinding
import com.dasoen.sga.model.Articulo

class ViewHolderArticulo(view: View): RecyclerView.ViewHolder(view){

    private val binding = ItemArticuloBinding.bind(view)

    @SuppressLint("CheckResult")
    fun render(articuloModel: Articulo, OnClickListener:(Articulo)-> Unit){
        binding.numArticuloList.text = articuloModel.numArticulo
        binding.desArticuloList.text = articuloModel.desArticulo
        binding.cdbArticulo.text = articuloModel.cdbArticulo
        binding.ivArticulo.setImageBitmap(articuloModel.imgArticulo)
        itemView.setOnClickListener{OnClickListener(articuloModel)}
    }
}