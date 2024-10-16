@file:Suppress("DEPRECATION")

package com.dasoen.sga.data

import android.annotation.SuppressLint
import java.text.SimpleDateFormat
import java.util.*

object BasicData {

    //cargar fecha para uso global de la app
    @SuppressLint("SimpleDateFormat")
    private var format = SimpleDateFormat("dd/M/yyyy")
    var date = format.format(Date()).toString()
    @SuppressLint("SimpleDateFormat")
    private var formatYear = SimpleDateFormat("yyyy")
    var year: String = formatYear.format(Date()).toString()

    var error_Login = ""

    //variables de acceso a la base de datos
    var ip = ""
    var dbGen = ""
    var dbGes = ""
    var port = ""


    //variable globales usuario
    var user = ""
    var pass = ""
    var name = ""
    var empresa = ""
    var RefPadreUser = ""
    var NumUsuario = ""

    //Movimiento
    var idMov = 0
    var idMovOnline = 0
    var fechaMov = ""
    var timeMov = ""
    var fechaMovTxt = ""
    var RefPadreMov = ""
    var tipoMov = 0
    var andenLogistica =""
    var origenLogistica =""
    var lugarDestino = ""
    var almacenOrigen = ""
    var almacenDestino = ""
    var desMovimiento = ""
    var Cerrado = 0

    //articulo
    var idArticulo = ""
    var desArticulo = ""
    var cdbArticulo = ""
    var orWareHouse = ""
    var desWareHouse = ""
    var numLin = 1
    var warehouseArray: ArrayList<String> = arrayListOf()
    var RefPadreLin = ""

    //ArticuloMov
    var idArticuloMov = ""
    var descArticuloMov = ""
    var cantidadArticuloMov = 0
    var origenArticuloMov = ""
    var destinoArticuloMov = ""
    var imagenArticuloMov = ""

    //TOKEN
    var token_access = ""

fun cleanData(){
    ip = ""
    dbGen = ""
    dbGes = ""
    port = ""
    user = ""
    pass = ""
    name = ""
    empresa = ""
    RefPadreUser = ""
    NumUsuario = ""
    idMov = 0
    idMovOnline = 0
    fechaMov = ""
    timeMov = ""
    fechaMovTxt = ""
    RefPadreMov = ""
    tipoMov = 0
    desMovimiento = ""
    Cerrado = 0
    idArticulo = ""
    desArticulo = ""
    cdbArticulo = ""
    orWareHouse = ""
    desWareHouse = ""
    numLin = 1
    warehouseArray = arrayListOf()
    RefPadreLin = ""
    idArticuloMov = ""
    descArticuloMov = ""
    cantidadArticuloMov = 0
    origenArticuloMov = ""
    destinoArticuloMov = ""
    imagenArticuloMov = ""
    token_access = ""
}
}