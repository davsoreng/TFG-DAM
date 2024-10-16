package com.dasoen.sga.adapterArticulo

import android.view.LayoutInflater
import android.view.ViewGroup
import androidx.recyclerview.widget.RecyclerView
import com.dasoen.sga.R
import com.dasoen.sga.model.Articulo

class AdapterArticulo(private val aList: List<Articulo>, private val OnClickListener:(Articulo)-> Unit): RecyclerView.Adapter<ViewHolderArticulo>() {

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): ViewHolderArticulo {
        val layoutInflater = LayoutInflater.from(parent.context)
        return ViewHolderArticulo(layoutInflater.inflate(R.layout.item_articulo, parent, false))
    }

    override fun getItemCount(): Int = aList.size

    override fun onBindViewHolder(holder: ViewHolderArticulo, position: Int) {
        val item = aList[position]
        holder.render(item, OnClickListener)
    }

}