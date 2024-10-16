package com.dasoen.sga.adapterArticuloMov

import android.view.View
import androidx.recyclerview.widget.RecyclerView
import com.dasoen.sga.databinding.ItemArticuloMovBinding
import com.dasoen.sga.model.ArticuloMov

class ViewHolderArticuloMov(view: View): RecyclerView.ViewHolder(view){

    private val binding = ItemArticuloMovBinding.bind(view)

    fun render(articulomovModel: ArticuloMov, OnClickListener:(ArticuloMov)-> Unit){
        binding.idArticulo.text = articulomovModel.idArticulo
        binding.desArticulo.text = articulomovModel.desArticulo
        binding.cantidadArticulo.text = articulomovModel.cantidadArticulo.toString()
        binding.origenArticulo.text = articulomovModel.almacenOrigen
        binding.destinoArticulo.text = articulomovModel.almacenDestino
        binding.imgArticulo.setImageBitmap(articulomovModel.imagenArticulo)
        //Glide.with(binding.ivArticulo).load(articuloModel.imgArticulo).load(binding.ivArticulo)
        itemView.setOnClickListener{OnClickListener(articulomovModel)}
    }
}