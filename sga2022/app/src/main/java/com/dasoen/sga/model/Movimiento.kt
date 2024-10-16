package com.dasoen.sga.model

import java.util.*

data class Movimiento(
    var idOrigen: Int,
    var RefPadre: String,
    var tipoMov: Int,
    var fechaMov: Date,
    var timeMov: String,
    var descripcionMov: String,
    var idOnline: Int,
    var servido: Int
    )