package com.dasoen.sga.movimientos

import android.os.Bundle
import androidx.appcompat.app.AppCompatActivity
import com.dasoen.sga.BuildConfig
import com.dasoen.sga.data.BasicData
import com.dasoen.sga.databinding.ActivityActProArticulosMovimientosInfoBinding

class ActProArticulosMovimientosInfo : AppCompatActivity() {

    private lateinit var binding: ActivityActProArticulosMovimientosInfoBinding

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityActProArticulosMovimientosInfoBinding.inflate(layoutInflater)
        setContentView(binding.root)

        //=============================
        //VALORES USUARIO + APP VERSION
        //=============================
        binding.versinoApp.text = BuildConfig.VERSION_NAME
        binding.userField.text = BasicData.name
        binding.dateField.text = BasicData.date

    }
}