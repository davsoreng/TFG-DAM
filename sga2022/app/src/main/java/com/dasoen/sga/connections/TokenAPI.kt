package com.dasoen.sga.connections

import com.dasoen.sga.data.BasicData
import com.squareup.okhttp.MediaType
import com.squareup.okhttp.OkHttpClient
import com.squareup.okhttp.Request
import com.squareup.okhttp.RequestBody
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch
import kotlinx.coroutines.runBlocking
import org.json.JSONObject
import org.json.JSONTokener

object TokenAPI {
    private var result: Boolean = false
    fun loginApi(user:String,pass:String):Boolean{
        runBlocking {
            launch(Dispatchers.IO) {
                val client = OkHttpClient()
                val body = "grant_type=password&username=$user&password=$pass"
                val credentialsApi: RequestBody = RequestBody.create(MediaType.parse("text/plain"), body)
                val request = Request.Builder()
                    .url("http://dsoriano.ddns.net/Login")
                    .post(credentialsApi)
                    .addHeader("Accept", "*/*")
                    .addHeader("Cache-Control", "no-cache")
                    .addHeader("Connection", "keep-alive")
                    .build()

                try {
                    val response = client.newCall(request).execute()
                    if(response.isSuccessful){
                        val responseBody = response.body()!!.string().trimIndent()
                        val jsonObject = JSONTokener(responseBody).nextValue() as JSONObject
                        BasicData.token_access = jsonObject.getString("access_token")
                        result = true
                    }
                }catch (e: Exception){
                    result = false
                    e.printStackTrace()
                }
            }
        }
        return if(result){
            result = false
            true
        }else{
            result = false
            false
        }

    }
}
