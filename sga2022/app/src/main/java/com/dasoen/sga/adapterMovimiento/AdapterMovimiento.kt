package com.dasoen.sga.adapterMovimiento

import android.view.LayoutInflater
import android.view.ViewGroup
import androidx.recyclerview.widget.RecyclerView
import com.dasoen.sga.R
import com.dasoen.sga.model.Movimiento

class AdapterMovimiento(private val mList: List<Movimiento>, private val OnClickListener:(Movimiento)->Unit):RecyclerView.Adapter<ViewHolderMovimiento>() {
    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): ViewHolderMovimiento {
        val layoutInflater = LayoutInflater.from(parent.context)
        return ViewHolderMovimiento(layoutInflater.inflate(R.layout.item_movimiento, parent, false))
    }

    override fun getItemCount(): Int = mList.size

    override fun onBindViewHolder(holder: ViewHolderMovimiento, position: Int) {
        val item = mList[position]
        holder.render(item, OnClickListener)
    }
}