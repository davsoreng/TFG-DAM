package com.dasoen.sga.adapterArticuloMov

import android.view.LayoutInflater
import android.view.ViewGroup
import androidx.recyclerview.widget.RecyclerView
import com.dasoen.sga.R
import com.dasoen.sga.model.ArticuloMov

class AdapterArticuloMov(private val amList: MutableList<ArticuloMov>, private val OnClickListener:(ArticuloMov)-> Unit): RecyclerView.Adapter<ViewHolderArticuloMov>() {

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): ViewHolderArticuloMov {
        val layoutInflater = LayoutInflater.from(parent.context)
        return ViewHolderArticuloMov(layoutInflater.inflate(R.layout.item_articulo_mov, parent, false))
    }

    override fun getItemCount(): Int = amList.size

    override fun onBindViewHolder(holder: ViewHolderArticuloMov, position: Int) {
        val item = amList[position]
        holder.render(item, OnClickListener)
    }
}