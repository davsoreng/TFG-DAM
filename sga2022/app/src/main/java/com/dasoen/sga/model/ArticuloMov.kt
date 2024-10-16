package com.dasoen.sga.model

import android.graphics.Bitmap

data class ArticuloMov(
    var idArticulo: String,
    var desArticulo: String,
    var cantidadArticulo: Int,
    var almacenDestino: String,
    var almacenOrigen: String,
    var referPadreArticulo: String,
    var imagenArticulo: Bitmap
    )