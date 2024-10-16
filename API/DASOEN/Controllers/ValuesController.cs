using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http;
using DASOEN;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Text;
// JVS 13-11-2019
// PONEMOS TIPOPROD EN EL MÉTODODO TraerDetallesArt


namespace DASOEN.Controllers
{
    [Authorize]
    public class ManteController : ApiController
    {
        public DASOEN.provider.SQLAdapter Sql = new DASOEN.provider.SQLAdapter();
        // GET http://localhost/DASOENDesarrollo/
        public object Get(String id, String IdAlmacen = "", String IdArticulo = "", String IdFamilia = "", String IdTipoArticulo = "",
            String IdTipologiaArticulo = "", String IdCodigo = "", String IdNivel = "", String IdUbicacion = "", String Stock = "", String IdSerie = "", String MailCliente = "", String RazonSocial = "", String CIFDNI = "",
            String TipoIVA = "", String Moneda = "", String Calle = "", String Colonia = "", String CodPostal = "", String Poblacion = "", String Provincia = "", String Pais = "", String NombreLocal = "", String Tlf1 = "",
            String Entidad = "", String Sucursal = "", String Cuenta = "", String DC = "", int LiquidaIVA = 0, String SeriePedido = "", String NumeroPedido = "",
            String Cliente = "", String Serie = "", String FechaPedido = "",
            String FechaConfirmacion = "", String FechaPedEntrega = "", String Agente = "", String SubAgente = "", String AgTransporte = "", String Transportista = "",
            String FormaPago = "", String Cambio = "", String DtoCabecera = "", String SuDocumento = "", String Observaciones = "", String Direccion = "",
            String Descripcion = "", String IdPedidoWeb = "", String Cantidad = "",
            String Precio = "", String Dto1 = "", String Dto2 = "", String Identificador = "", String UltimaLinea = "", String Cif = "", String Telefono_Casa = "", String Telefono_Cel = "",
            String TipoTarifa = "", String NumLinea = "", String BD = "", String CostoEnvio = "", String CodPostalFra = "",
            String CalleFra = "", String ColoniaFra = "", String PoblacionFra = "", String ProvinciaFra = "", String PaisFra = "", String Nombre = "", String Partida = "", String Facturar = "",
            String UdMetrica = "", String IdColeccion = "", String IdLinea = "", String IdTam = "", String UsoCFDi = "", String Cobro_CobradoPor = "", String Cobro_Estatus = "", String Cobro_FechaHoraAcreditado = "", String
            Cobro_idTransaccion = "", String Cobro_Importe = "", String Cobro_Comision = "", String Cobro_ImporteNeto = "", String Cobro_MP_TipoCobro = "", String Cobro_MP_TipoTarjeta = "", String Cobro_MP_TipoCuenta = "",
            String Cobro_Observaciones = "", String CadArticulos = "", String Periodo = "", String MetodoEnvio = "", String Maquina = "", String Usuario = "", String DatosPartidas = "", String RFC = "",
            String FechaCancelacion = "", String StringPedidosCancelados = "", String GiftCard = "", String ListaPedidos = "", String StringCadena = "", String anyo = "", String tipo = "", String query = "",
             String Empresa = "", String Formulario = "", String Tabla = "", String CampoAContar = "", String BDCliente = "", String AlmacenOrigen = "", String AlmacenDestino = "", String Articulo = "",
            String EstadoLogistica = "", String TipoMovimiento = "", String OrigenMovimiento = "", String LugarDestino = "", String Movimiento = "", String FechaMov = "", String bdcliente = "",
             String AndenLogistica = "", String OrigenLogistica = "", String Servido = "", String RefPadre = "", String IdMovimiento = "", String CBD = "", String idArticulo = "", String Lote = "", String CRU = "",
             String numArticulo = "", String Tipo = "", String CDB = "", String NombrePersona="", String Contrasenya="", String Referencia="", String Linea="", String CDBPedido="")
        {
            if (id == Metodos.TraerAlmacenesTodos.method)
            {
                return GetAlmacenes();
            }
            if (id == Metodos.TraerAlmacen.method)
            {
                return GetAlmacen(IdAlmacen);
            }
            if (id == Metodos.TraerProdAlmWeb.method)
            {
                return ProdAlmWeb();
            }
            if (id == Metodos.TraerDetallesArt.method)
            {
                return TraerDetallesArt(IdArticulo, IdFamilia);
            }
            if (id == Metodos.TraerProducto.method)
            {
                return Producto(IdArticulo);
            }
            if (id == Metodos.TraerFamiliasTodas.method)
            {
                return FamiliasTodas();
            }
            if (id == Metodos.TraerFamilia.method)
            {
                return Familia(IdFamilia);
            }
            if (id == Metodos.TraerTiposArticulosTodos.method)
            {
                return TiposArticulosTodos();
            }
            if (id == Metodos.TraerTipoArticulo.method)
            {
                return TipoArticulo(IdTipoArticulo);
            }
            if (id == Metodos.TraerTipologiasTodas.method)
            {
                return TipologiasTodas();
            }
            if (id == Metodos.TraerTipologia.method)
            {
                return Tipologia(IdTipologiaArticulo);
            }
            if (id == Metodos.TraerTipologiasTodasLin.method)
            {
                return TipologiasTodasLin();
            }
            if (id == Metodos.TraerTipologiaLin.method)
            {
                return TipologiaLin(IdTipologiaArticulo, IdNivel);
            }
            if (id == Metodos.TraerTipologiasTodasLinLin.method)
            {
                return TipologiasTodasLinLin();
            }
            if (id == Metodos.TraerTipologiaLinLin.method)
            {
                return TipologiaLinLin(IdCodigo, IdTipologiaArticulo, IdNivel);
            }
            if (id == Metodos.TraerTarifasTodas.method)
            {
                return TraerTarifasTodas();
            }
            if (id == Metodos.TraerTarifa.method)
            {
                return TraerTarifa(IdArticulo);
            }
            if (id == Metodos.TraerSeriesTodas.method)
            {
                return TraerSeriesTodas();
            }
            if (id == Metodos.TraerSerie.method)
            {
                return TraerSerie(IdSerie);
            }
            /*if (id == Metodos.EnviarSerie)
            {
                return "En desarrollo";
            }*/
            if (id == Metodos.EnviarPedido.method)
            {
                /*return EnviarPedido(Identificador, FechaPedido, Moneda, Cambio, Observaciones, Nombre, MailCliente, Calle, Colonia,
            CodPostal, Poblacion, Provincia, Pais, Telefono_Casa, Telefono_Cel, Cif, RazonSocial, CodPostalFra,
            CalleFra, ColoniaFra, PoblacionFra, ProvinciaFra, PaisFra, TipoTarifa, NumLinea, IdArticulo, Descripcion,
            Cantidad, Precio, Dto1, BD, CostoEnvio, UltimaLinea);*/
                return EnviarPedido(Identificador, FechaPedido, Cliente, Moneda, Cambio, Observaciones, Nombre, MailCliente, Calle, Colonia,
            CodPostal, Poblacion, Provincia, Pais, Telefono_Casa, Telefono_Cel, Facturar, Cif, RazonSocial, CalleFra,
            ColoniaFra, CodPostalFra, PoblacionFra, ProvinciaFra, PaisFra, UsoCFDi, TipoTarifa, Partida, IdArticulo, Descripcion,
            Cantidad, Precio, Dto1, UdMetrica, BD, CostoEnvio, Cobro_CobradoPor, Cobro_Estatus, Cobro_FechaHoraAcreditado,
            Cobro_idTransaccion, Cobro_Importe, Cobro_Comision, Cobro_ImporteNeto, Cobro_MP_TipoCobro, Cobro_MP_TipoTarjeta, Cobro_MP_TipoCuenta, Cobro_Observaciones, UltimaLinea, MetodoEnvio, Sucursal, Maquina, Usuario, GiftCard);
            }
            if (id == Metodos.EnviarPedidosCompleto.method)
            {
                return EnviarPedidosCompleto(Maquina, Usuario, Identificador, FechaPedido, Cliente, Moneda, Cambio,
                    Nombre, MailCliente, Calle, Colonia, CodPostal, Poblacion, Provincia, Pais, Telefono_Casa, Facturar, Cif, RazonSocial,
                    CalleFra, ColoniaFra, CodPostalFra, PoblacionFra, ProvinciaFra, PaisFra, UsoCFDi, TipoTarifa, DatosPartidas,
                    MetodoEnvio, Sucursal, Cobro_CobradoPor, Cobro_Estatus, Cobro_FechaHoraAcreditado, Cobro_idTransaccion, Cobro_Importe,
                    Cobro_Comision, Cobro_ImporteNeto, Cobro_MP_TipoCobro, Cobro_MP_TipoTarjeta, Cobro_MP_TipoCuenta, Cobro_Observaciones, GiftCard);
                //     return EnviarPedidosCompleto(Identificador, FechaPedido, Cliente, Moneda, Cambio, Observaciones, Nombre, MailCliente, Calle, Colonia,
                //CodPostal, Poblacion, Provincia, Pais, Telefono_Casa, Telefono_Cel, Facturar, Cif, RazonSocial, CalleFra,
                //ColoniaFra, CodPostalFra, PoblacionFra, ProvinciaFra, PaisFra, UsoCFDi, TipoTarifa, Partida, IdArticulo, Descripcion,
                //Cantidad, Precio, Dto1, UdMetrica, BD, CostoEnvio, Cobro_CobradoPor, Cobro_Estatus, Cobro_FechaHoraAcreditado,
                //Cobro_idTransaccion, Cobro_Importe, Cobro_Comision, Cobro_ImporteNeto, Cobro_MP_TipoCobro, Cobro_MP_TipoTarjeta, Cobro_MP_TipoCuenta, Cobro_Observaciones, UltimaLinea, MetodoEnvio, Sucursal, Maquina, Usuario);
                //
            }
            if (id == Metodos.TraerPedido.method)
            {
                return TraerPedido(Identificador);
            }
            if (id == Metodos.TraerTraking.method)
            {
                return "En desarrollo";
            }
            if (id == Metodos.TraerImagen.method)
            {
                return TraerImagen(IdArticulo);
            }
            if (id == Metodos.TraerMetodosPago.method)
            {
                return TraerMetodosPago();
            }
            if (id == Metodos.ExisteCliente.method)
            {
                return ExisteCliente(MailCliente);
            }
            if (id == Metodos.TraerCliente.method)
            {
                return TraerCliente(MailCliente);
            }
            if (id == Metodos.AltaCliente.method)
            {
                return AltaCliente(RazonSocial, CIFDNI, TipoIVA, Moneda, Calle, Colonia, CodPostal, Poblacion, Provincia, Pais, NombreLocal, Tlf1, MailCliente, Entidad, Sucursal, Cuenta, DC, LiquidaIVA);
            }
            if (id == Metodos.TraerMonedas.method)
            {
                return TraerMonedas();
            }
            if (id == Metodos.TraerMoneda.method)
            {
                return TraerMoneda(Moneda);
            }
            if (id == Metodos.GenerarDevolucion.method)
            {
                return "En desarrollo";
            }
            if (id == Metodos.TraerStock.method)
            {
                return TraerStock(IdArticulo);
            }
            if (id == Metodos.EnviarStock.method)
            {
                return EnviarStock(IdArticulo, IdAlmacen, IdUbicacion, Stock);
            }
            if (id == Metodos.TraerAtributosArt.method)
            {
                return TraerAtributosArt(IdArticulo);
            }
            if (id == Metodos.TraerAtributosLineas.method)
            {
                return TraerAtributosLineas(IdLinea);
            }
            if (id == Metodos.TraerArticulosColecciones.method)
            {
                return TraerArticulosColecciones(IdColeccion, IdLinea);
            }
            if (id == Metodos.TraerArticulosTam.method)
            {
                return TraerArticulosTam(IdTam);
            }
            if (id == Metodos.TraerValidarStock.method)
            {
                return TraerValidarStock(Sql.IdEmpresa, Periodo, CadArticulos);
            }

            if (id == Metodos.RecibePedidosCancelados.method)
            {
                return RecibePedidosCancelados(Sql.IdEmpresa, Periodo, RFC, FechaCancelacion, StringPedidosCancelados);
            }

            //if (id == Metodos.ConsultaEstatusPedidos.method)
            //{
            //    return ConsultaEstatusPedidos(Sql.IdEmpresa, Periodo, ListaPedidos, StringCadena);
            //}

            if (id == Metodos.TraerMovimientos.method)
            {
                return TraerMovimientos(anyo);
            }

            if (id == Metodos.TraerMovimientosFiltro.method)
            {
                return TraerMovimientosFiltro(anyo, tipo, query);
            }

            if (id == Metodos.TraerContadorMovimientos.method)
            {
                return TraerContadorMovimientos(Empresa, Periodo, Serie, Formulario, Tabla, CampoAContar);
            }

            if (id == Metodos.CrearCabeceraMovimientos.method)
            {
                return CrearCabeceraMovimientos(Empresa, Periodo, Movimiento, FechaMov, Usuario, bdcliente, AndenLogistica, OrigenLogistica,
            Observaciones, EstadoLogistica, TipoMovimiento, OrigenMovimiento, LugarDestino, AlmacenOrigen, AlmacenDestino, Servido);
                //return CrearCabeceraMovimientos(Empresa, Periodo, Movimiento, FechaMov, Usuario, bdcliente, Observaciones, EstadoLogistica, TipoMovimiento, OrigenMovimiento, Servido);
            }

            if (id == Metodos.CrearLineasMovimientos.method)
            {
                return CrearLineasMovimientos(Empresa, Periodo, Referencia, Movimiento, Linea, FechaMov, Cantidad, Articulo,
            Descripcion, AlmacenOrigen, AlmacenDestino, AndenLogistica, EstadoLogistica, bdcliente, CDBPedido, LugarDestino, CRU);
            }

            if (id == Metodos.TraerArticulosFiltroArticuloCBD.method)
            {
                return TraerArticulosFiltroArticuloCBD(Articulo, CDB);
            }

            if (id == Metodos.TraerArticulos.method)
            {
                return TraerArticulos();
            }

            if (id == Metodos.TraerArticulosTipo.method)
            {
                return TraerArticulosTipo(Tipo,query);
            }

            if (id == Metodos.TraerMovimientoId.method)
            {
                return TraerMovimientoId(IdMovimiento);
            }

            if (id == Metodos.TraerArticulosMovimientoId.method)
            {
                return TraerArticulosMovimientoId(IdMovimiento);
            }

            if (id == Metodos.EliminarMovimiento.method)
            {
                return EliminarMovimiento(Empresa, bdcliente, RefPadre);
            }

            if (id == Metodos.ActualizarMovimiento.method)
            {
                return ActualizarMovimiento(bdcliente, Descripcion, RefPadre, Servido);
            }

            if (id == Metodos.TraerImagenArticulo.method)
            {
                return TraerImagenArticulo(numArticulo);
            }

            if (id == Metodos.TraerImagenArticuloMovimientoId.method)
            {
                return TraerImagenArticuloMovimientoId(IdMovimiento, idArticulo);
            }


            if (id == Metodos.ActualizarArticuloMovimiento.method)
            {
                return ActualizarArticuloMovimiento(BDCliente, AlmacenOrigen, AlmacenDestino, Articulo, Descripcion, Lote, CRU, Cantidad, RefPadre);
            }

            if (id == Metodos.EliminarArticuloMovimiento.method)
            {
                return EliminarArticuloMovimiento(Empresa, BDCliente, Movimiento, RefPadre);
            }

            if (id == Metodos.TraerArticuloDescripcion.method)
            {
                return TraerArticuloDescripcion(Descripcion);
            }

            if (id == Metodos.TraerInfoUsuario.method)
            {
                return TraerInfoUsuario(NombrePersona,Contrasenya);
            }

            return "No se ha encontrado ningun metodo compatible";
        }
        public HttpResponseMessage TraerImagen(String IdArticulo)
        {
            String sMensajeError = "";
            //select Empresa,Articulo,Descripcion,Imagen,DescripcionImagen,RefImg from vTraerImagenes"
            string sql = "select RefImg from " + Metodos.TraerImagen.proc;
            //string sql = "select RefImg from vTraerImagenes";
            Byte[] iResponse = Sql.TraeImagenes(sql, new String[] { "Empresa", "Articulo" }, new String[] { Sql.IdEmpresa, IdArticulo });
            MemoryStream ms = new MemoryStream(iResponse);
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StreamContent(ms);
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/png");
            return response;
        }

        //public JArray EnviarPedido(String Identificador, String FechaPedido, String Moneda, String Cambio, String Observaciones, String Nombre, String MailCliente, String Calle, String Colonia,
        //    String CodPostal, String Poblacion, String Provincia, String Pais, String Telefono_Casa, String Telefono_Cel, String CIF, String RazonSocial, String CodPostalFra, String Partida,
        //    String CalleFra, String ColoniaFra, String PoblacionFra, String ProvinciaFra, String PaisFra, String TipoTarifa, String IdArticulo, String Descripcion,
        //    String Cantidad, String Precio, String Dto1, String BD, String CostoEnvio, String UltimaLinea)
        //{
        //    String sMensajeError = "";
        //    string Table = "sp_PedWebPAmigaRecibePedido";//"prc_ges_GenVentasDesdePedidosWeb";
        //    /* string sResponse = Sql.EjecutaProcAlm(Table, new String[] { "Identificador" ,"Empresa" , "Periodo" , "Usuario" , "Maquina" , "Cliente" , "Serie" , "FechaPedido" , "FechaConfirmacion" ,
        //     "FechaPedEntrega" , "Agente" , "SubAgente" , "AgTransporte" , "Transportista" , "FormaPago" , "Moneda " , "Cambio" , "DtoCabecera" , "SuDocumento" , "Observaciones " ,
        //     "NombreLocal" , "Calle" , "Colonia" , "CodPostal" , "Poblacion" , "Provincia" , "Pais" , "Almacen" , "Ubicacion" , "Articulo" , "Descripcion" ,
        //         "Cantidad" , "Precio" , "Dto1" , "Dto2","UltimaLinea"},
        //     new String[] { Identificador ,Sql.IdEmpresa, "2018", "0","Web", Cliente , Serie , FechaPedido , FechaConfirmacion , FechaPedEntrega , Agente , SubAgente , AgTransporte ,
        //         Transportista , FormaPago , Moneda  , Cambio , DtoCabecera , SuDocumento , Observaciones  , NombreLocal , Calle , Colonia , CodPostal , Poblacion ,
        //         Provincia , Pais , Almacen , Ubicacion , Articulo , Descripcion ,Cantidad , Precio , Dto1 , Dto2, UltimaLinea });*/
        //    string sResponse = Sql.EjecutaProcAlm(Table, new String[] { "Maquina","Usuario","Empresa","Periodo","IdPedido","FechaPedido","Moneda","Cambio","Observaciones","Nombre","Email","Calle","Colonia",
        //    "CodPostal","Poblacion","Provincia","Pais","Telefono_Casa","Telefono_Cel","Facturar","CIFDNI","RazonSocial","CalleFra","ColoniaFra","CodPostalFra","PoblacionFra","ProvinciaFra","PaisFra","TipoTarifa",
        //    "Partida","Articulo","Descripcion","Cantidad","Precio","Dto1","BD","CostoEnvio","UltimaLinea"},
        //    new String[] { "Web", "0", Sql.IdEmpresa, "2018",Identificador,FechaPedido,Moneda,Cambio,Observaciones,Nombre,MailCliente,Calle,Colonia,
        //    CodPostal,Poblacion,Provincia,Pais,Telefono_Casa,Telefono_Cel,"0",CIF,RazonSocial,CalleFra,ColoniaFra,CodPostalFra,PoblacionFra,ProvinciaFra,PaisFra,TipoTarifa,
        //    Partida,IdArticulo,Descripcion,Cantidad,Precio,Dto1,BD,CostoEnvio,UltimaLinea });


        //    return sResponse;
        //}
        /*public JArray EnviarPedido(String Identificador, String FechaPedido, String Moneda, String Cambio, String Observaciones, String Nombre, String MailCliente, String Calle, String Colonia,
    String CodPostal, String Poblacion, String Provincia, String Pais, String Telefono_Casa, String Telefono_Cel, String CIF, String RazonSocial, String CodPostalFra, String Partida,
    String CalleFra, String ColoniaFra, String PoblacionFra, String ProvinciaFra, String PaisFra, String TipoTarifa,String Facturar, String IdArticulo, String Descripcion,
    String Cantidad,String UdMetrica, String Precio, String Dto1,String Cliente, String BD, String CostoEnvio, String UltimaLinea)*/
        public JArray EnviarPedido(String Identificador, String FechaPedido, String Cliente, String Moneda, String Cambio, String Observaciones, String Nombre, String MailCliente, String Calle, String Colonia,
            String CodPostal, String Poblacion, String Provincia, String Pais, String Telefono_Casa, String Telefono_Cel, String Facturar, String CIF, String RazonSocial, String CalleFra,
            String ColoniaFra, String CodPostalFra, String PoblacionFra, String ProvinciaFra, String PaisFra, String UsoCFDi, String TipoTarifa, String Partida, String IdArticulo, String Descripcion,
            String Cantidad, String Precio, String Dto1, String UdMetrica, String BD, String CostoEnvio, String Cobro_CobradoPor, String Cobro_Estatus, String Cobro_FechaHoraAcreditado,
            String Cobro_idTransaccion, String Cobro_Importe, String Cobro_Comision, String Cobro_ImporteNeto, String Cobro_MP_TipoCobro, String Cobro_MP_TipoTarjeta, String Cobro_MP_TipoCuenta,
            String Cobro_Observaciones, String UltimaLinea, String MetodoEnvio, String Sucursal, String Maquina, String Usuario, String GiftCard)
        {
            //"prc_ges_GenVentasDesdePedidosWeb";
            /* string sResponse = Sql.EjecutaProcAlm(Table, new String[] { "Identificador" ,"Empresa" , "Periodo" , "Usuario" , "Maquina" , "Cliente" , "Serie" , "FechaPedido" , "FechaConfirmacion" ,
             "FechaPedEntrega" , "Agente" , "SubAgente" , "AgTransporte" , "Transportista" , "FormaPago" , "Moneda " , "Cambio" , "DtoCabecera" , "SuDocumento" , "Observaciones " ,
             "NombreLocal" , "Calle" , "Colonia" , "CodPostal" , "Poblacion" , "Provincia" , "Pais" , "Almacen" , "Ubicacion" , "Articulo" , "Descripcion" ,
                 "Cantidad" , "Precio" , "Dto1" , "Dto2","UltimaLinea"},
             new String[] { Identificador ,Sql.IdEmpresa, "2018", "0","Web", Cliente , Serie , FechaPedido , FechaConfirmacion , FechaPedEntrega , Agente , SubAgente , AgTransporte ,
                 Transportista , FormaPago , Moneda  , Cambio , DtoCabecera , SuDocumento , Observaciones  , NombreLocal , Calle , Colonia , CodPostal , Poblacion ,
                 Provincia , Pais , Almacen , Ubicacion , Articulo , Descripcion ,Cantidad , Precio , Dto1 , Dto2, UltimaLinea });*/
            /*string sResponse = Sql.EjecutaProcAlm(Table, new String[] { "Maquina","Usuario","Empresa","Periodo","IdPedido","Cliente","FechaPedido","Moneda","Cambio","Observaciones","Nombre","Email","Calle",
                "Colonia","CodPostal","Poblacion","Provincia","Pais","Telefono_Casa","Telefono_Cel","Facturar","CIFDNI","RazonSocial","CalleFra","ColoniaFra","CodPostalFra","PoblacionFra",
                "ProvinciaFra","PaisFra","TipoTarifa","Partida","Articulo","Descripcion","Cantidad","Precio","Dto1","UdMetrica","Cliente","BD","CostoEnvio","UltimaLinea"
            },*/
            /*new String[] { "Web","0",Sql.IdEmpresa,"2018",Identificador,FechaPedido,Moneda,Cambio,Observaciones,Nombre,MailCliente,Calle,Colonia,CodPostal,Poblacion,Provincia,Pais,Telefono_Casa,Telefono_Cel,
    Facturar,CIF,RazonSocial,CalleFra,ColoniaFra,CodPostalFra,PoblacionFra,ProvinciaFra,PaisFra,TipoTarifa,Partida,IdArticulo,Descripcion,Cantidad,Precio,Dto1,UdMetrica,Cliente,BD,
    CostoEnvio,UltimaLinea
}*/

            if (GiftCard.Split('|').Length > 2)
            {
                StringBuilder sb = new StringBuilder();
                StringWriter sw = new StringWriter(sb);
                JsonWriter writer = new JsonTextWriter(sw);
                writer.WriteStartArray();
                writer.WriteStartObject();
                writer.WritePropertyName("response");
                writer.WriteValue("El valor recibido en GiftCard no es correcto.");
                writer.WriteEndObject();
                writer.WriteEndArray();
                return JArray.Parse(writer.ToString());
            }



            String sMensajeError = "";
            //string Table = "sp_PedWebPAmigaRecibePedido";
            string Table = Metodos.EnviarPedido.proc;
            string sResponse = Sql.EjecutaProcAlm(Table, new String[] {"Maquina","Usuario","Empresa","Periodo","IdPedido","FechaPedido","Cliente","Moneda","Cambio","Observaciones","Nombre","Email","Calle",
                        "Colonia","CodPostal","Poblacion","Provincia","Pais","Telefono_Casa","Telefono_Cel","Facturar","CIFDNI","RazonSocial","CalleFra","ColoniaFra","CodPostalFra","PoblacionFra",
                        "ProvinciaFra","PaisFra","UsoCFDi","TipoTarifa","Partida","Articulo","Descripcion","Cantidad","Precio","Dto1","UdMetrica","BD","CostoEnvio","MetodoEnvio","Sucursal","Cobro_CobradoPor","Cobro_Estatus",
                        "Cobro_FechaHoraAcreditado","Cobro_idTransaccion","Cobro_Importe","Cobro_Comision","Cobro_ImporteNeto","Cobro_MP_TipoCobro","Cobro_MP_TipoTarjeta","Cobro_MP_TipoCuenta","Cobro_Observaciones",
                        "UltimaLinea","GiftCard"},

            new string[]
            {
                        Maquina,Usuario,Sql.IdEmpresa,DateTime.Now.Year.ToString(),Identificador,FechaPedido,Cliente,Moneda,Cambio,Observaciones,Nombre,MailCliente,Calle,Colonia,CodPostal,Poblacion,Provincia,Pais,Telefono_Casa,Telefono_Cel,
                        Facturar,CIF,RazonSocial,CalleFra,ColoniaFra,CodPostalFra,PoblacionFra,ProvinciaFra,PaisFra,UsoCFDi,TipoTarifa,Partida,IdArticulo,Descripcion,Cantidad,Precio,Dto1,UdMetrica,
                        BD,CostoEnvio,MetodoEnvio,Sucursal,Cobro_CobradoPor,Cobro_Estatus,Cobro_FechaHoraAcreditado,Cobro_idTransaccion,Cobro_Importe,Cobro_Comision,Cobro_ImporteNeto,Cobro_MP_TipoCobro,Cobro_MP_TipoTarjeta,
                        Cobro_MP_TipoCuenta,Cobro_Observaciones,UltimaLinea,GiftCard
            });


            return JArray.Parse(sResponse);
        }
        public JArray TraerPedido(String Identificador)
        {
            String sMensajeError = "";
            //string sql = "select Empresa,Periodo,Tipo,Serie,Numero,FechaPedido,FechaConfirmacion,Cliente,FormaPago,Observaciones,Moneda,Cambio,AgTransporte,Calle,Colonia,CodPostal,Poblacion,Provincia,Pais,NombreLocal,Articulo,Descripcion,Cantidad,Precio,Importe,EstadoLogistica,CodigoColeccion,DescripcionColeccion,CodigoRelleno,DescripcionRelleno,CodigoMedida,DescripcionMedida,CodigoLinea,DescripcionLinea,CodigoTamanos,DescripcionTamanos,CodigoTela,DescripcionTela,IdPedidoWeb from VTraerVentasPedidos";
            string sql = "select Empresa,Periodo,Tipo,Serie,Numero,FechaPedido,FechaConfirmacion,Cliente,FormaPago,Observaciones,Moneda,Cambio,AgTransporte,Calle,Colonia,CodPostal,Poblacion,Provincia,Pais,NombreLocal,Articulo,Descripcion,Cantidad,Precio,Importe,EstadoLogistica,CodigoColeccion,DescripcionColeccion,CodigoRelleno,DescripcionRelleno,CodigoMedida,DescripcionMedida,CodigoLinea,DescripcionLinea,CodigoTamanos,DescripcionTamanos,CodigoTela,DescripcionTela,IdPedidoWeb from " + Metodos.TraerPedido.proc;
            string sResponse = Sql.TraeValores(sql, new String[] { "Empresa", "Periodo", "IdPedidoWeb" }, new String[] { Sql.IdEmpresa, DateTime.Now.Year.ToString(), Identificador });
            return JArray.Parse(sResponse);
        }
        public JArray TraerAtributosArt(String IdArticulo)
        {
            String sMensajeError = "";
            //string sql = "select Articulo,Descripcion,CodigoColeccion,DescripcionColeccion,CodigoRelleno,DescripcionRelleno,CodigoMedida,DescripcionMedida,CodigoLinea,DescripcionLinea,CodigoTamanos,DescripcionTamanos,CodigoTela,DescripcionTela from VTraerAriculosAtributos";
            string sql = "select Articulo,Descripcion,CodigoColeccion,DescripcionColeccion,CodigoRelleno,DescripcionRelleno,CodigoMedida,DescripcionMedida,CodigoLinea,DescripcionLinea,CodigoTamanos,DescripcionTamanos,CodigoTela,DescripcionTela from " + Metodos.TraerAtributosArt.proc;
            string sResponse = Sql.TraeValores(sql, new String[] { "Empresa", "Articulo" }, new String[] { Sql.IdEmpresa, IdArticulo });
            return JArray.Parse(sResponse);
        }
        public JArray TraerAtributosLineas(String IdLinea)
        {
            String sMensajeError = "";
            //string sql = "select CodigoLinea,DescripcionLinea,Observaciones,Orden,Baja,VentaOnLine from vTraerArticulosLineas";
            string sql = "select CodigoLinea,DescripcionLinea,Observaciones,Orden,Baja,VentaOnLine from " + Metodos.TraerAtributosLineas.proc;
            string sResponse = Sql.TraeValores(sql, new String[] { "Empresa", "CodigoLinea" }, new String[] { Sql.IdEmpresa, IdLinea });
            return JArray.Parse(sResponse);
        }
        public JArray TraerArticulosColecciones(String IdColeccion, String IdLinea)
        {
            String sMensajeError = "";
            //string sql = "select CodigoLinea,DescripcionLinea,CodigoColeccion,DescripcionColeccion,Baja,VentaOnLine from vTraerArticulosColecciones";
            string sql = "select CodigoLinea,DescripcionLinea,CodigoColeccion,DescripcionColeccion,Baja,VentaOnLine from " + Metodos.TraerArticulosColecciones.proc;
            string sResponse = Sql.TraeValores(sql, new String[] { "Empresa", "CodigoLinea", "CodigoColeccion" }, new String[] { Sql.IdEmpresa, IdLinea, IdColeccion });
            return JArray.Parse(sResponse);
        }
        public JArray TraerArticulosTam(String IdTam)
        {
            String sMensajeError = "";
            //string sql = "select CodigoTamano,DescripcionTamano,Baja,VentaOnLine from vTraerArticulosTam";
            string sql = "select CodigoTamano,DescripcionTamano,Baja,VentaOnLine from " + Metodos.TraerArticulosTam.proc;
            string sResponse = Sql.TraeValores(sql, new String[] { "Empresa", "CodigoTamano" }, new String[] { Sql.IdEmpresa, IdTam });
            return JArray.Parse(sResponse);
        }
        //public JArray TraerDetallesArt(String IdArticulo, String IdFamilia)
        //{
        //    String sMensajeError = "";
        //    string sResponse = Sql.EjecutaProcAlm(Metodos.TraerDetallesArt.proc, new String[] { "Empresa", "Articulo", "Familia" },
        //         new String[] { Sql.IdEmpresa, IdArticulo, IdFamilia.ToString() });

        //    try
        //    {
        //        JObject jResponse = JObject.Parse(sResponse);
        //        sResponse = jResponse.ToString();
        //    }
        //    catch (Exception Ex)
        //    {
        //    }
        //    return sResponse;
        //}

        public JArray TraerDetallesArt(String IdArticulo, String IdFamilia)
        {
            //String sMensajeError = "";
            //string sql = "select Articulo,Descripcion,Familia,DescripcionFamilia,TipoArticulo,DescripcionTipoArticulo,CodigoTamano,DescripcionTamano,CodigoTelaA," +
            //    "DescripcionTelA,CodigoTelaB,DescripcionTelB,CodigoLinea,DescripcionLinea,CodigoMedida,DescripcionMedida,CodigoRelleno,DescripcionRelleno,CodigoColeccion," +
            //    "DescripcionColeccion,Stock,Tarifa,CodigoAlmacen,DescripcionAlmacen,PrecioNormal,PrecioPremium,GastoEnvio,DescripcionComercial,DescripcionHTML,BD,CDB from vTraerDetallesArt";

            //string sql = "select Articulo,Descripcion,Familia,DescripcionFamilia,TipoArticulo,DescripcionTipoArticulo,CodigoTamano,DescripcionTamano,CodigoTelaA," +
            //"DescripcionTelA,CodigoTelaB,DescripcionTelB,CodigoLinea,DescripcionLinea,CodigoMedida,DescripcionMedida,CodigoRelleno,DescripcionRelleno,CodigoColeccion," +
            //"DescripcionColeccion,Stock,Tarifa,CodigoAlmacen,DescripcionAlmacen,PrecioNormal,PrecioPremium,GastoEnvio,DescripcionComercial,DescripcionHTML,BD,CDB,TipoProd from " +
            //string sql = "select * from " +
            // Metodos.TraerDetallesArt.proc;
            //string sResponse = "";
            //String[] Campos = new String[0];
            //String[] Valores = new String[0];
            //if (IdArticulo != "" && IdArticulo != null)
            //{
            //    Array.Resize(ref Campos, Campos.Length + 1);
            //    Array.Resize(ref Valores, Valores.Length + 1);
            //    Campos[Campos.Length - 1] = "Articulo";
            //    Valores[Valores.Length - 1] = IdArticulo;
            //}
            //{
            //if (IdFamilia != "" && IdFamilia != null)
            //    Array.Resize(ref Campos, Campos.Length + 1);
            //    Array.Resize(ref Valores, Valores.Length + 1);
            //    Campos[Campos.Length - 1] = "Familia";
            //    Valores[Valores.Length - 1] = IdFamilia;
            //}
            String sMensajeError = "";
            string sql = "select * from " + Metodos.TraerDetallesArt.proc;
            //if (Campos.Length < 1) { return ""; }
            //string sResponse = Sql.TraeValores(sql, Campos, Valores);
            string sResponse = Sql.TraeValores(sql, new String[] { "Empresa" }, new String[] { Sql.IdEmpresa });
            return JArray.Parse(sResponse);
        }
        public JArray TraerMoneda(String CodMoneda)
        {
            String sMensajeError = "";
            //string sql = "select Moneda,Descripcion from vTraerMoneda";
            string sql = "select Moneda,Descripcion from " + Metodos.TraerMoneda.proc;
            string sResponse = Sql.TraeValores(sql, new String[] { "Empresa", "Moneda" }, new String[] { Sql.IdEmpresa, CodMoneda });
            return JArray.Parse(sResponse);
        }
        public JArray TraerMonedas()
        {
            String sMensajeError = "";
            //string sql = "select Moneda,Descripcion from vTraerMoneda";
            string sql = "select Moneda,Descripcion from " + Metodos.TraerMonedas.proc;
            string sResponse = Sql.TraeValores(sql, new String[] { "Empresa" }, new String[] { Sql.IdEmpresa });
            return JArray.Parse(sResponse);
        }
        public JArray TraerMetodosPago()
        {
            String sMensajeError = "";
            //string sql = "select TipoPago,Descripcion from vTraerTiposPago"; // NO VA POR PERIODO
            string sql = "select TipoPago,Descripcion from " + Metodos.TraerMetodosPago.proc; // NO VA POR PERIODO
            string sResponse = Sql.TraeValores(sql, new String[] { "Empresa" }, new String[] { Sql.IdEmpresa });
            return JArray.Parse(sResponse);
        }
        public JArray AltaCliente(String RazonSocial, String CIFDNI, String TipoIVA, String Moneda, String Calle, String Colonia,
            String CodPostal, String Poblacion, String Provincia, String Pais, String NombreLocal, String Tlf1, String Email, String Entidad, String Sucursal, String Cuenta, String DC, int LiquidaIVA)
        {
            String sMensajeError = "";
            string Table = "MaeStock";
            //string sResponse = Sql.EjecutaProcAlm("prc_ges_CreaClientes", new String[] { "Empresa", "RazonSocial", "CIFDNI", "TipoIVA", "Moneda", "Calle", "Colonia",
            //    "CodPostal", "Poblacion", "Provincia", "Pais", "NombreLocal", "Tlf1", "Email", "Entidad", "Sucursal", "Cuenta", "DC", "LiquidaIVA" },
            //    new String[] { Sql.IdEmpresa, RazonSocial, CIFDNI, TipoIVA, Moneda, Calle, Colonia, CodPostal, Poblacion, Provincia, Pais, NombreLocal, Tlf1, Email, Entidad, Sucursal, Cuenta, DC, LiquidaIVA.ToString() });
            string sResponse = Sql.EjecutaProcAlm(Metodos.AltaCliente.proc, new String[] { "Empresa", "RazonSocial", "CIFDNI", "TipoIVA", "Moneda", "Calle", "Colonia",
                "CodPostal", "Poblacion", "Provincia", "Pais", "NombreLocal", "Tlf1", "Email", "Entidad", "Sucursal", "Cuenta", "DC", "LiquidaIVA" },
                 new String[] { Sql.IdEmpresa, RazonSocial, CIFDNI, TipoIVA, Moneda, Calle, Colonia, CodPostal, Poblacion, Provincia, Pais, NombreLocal, Tlf1, Email, Entidad, Sucursal, Cuenta, DC, LiquidaIVA.ToString() });

            try
            {
                JObject jResponse = JObject.Parse(sResponse);
                string sResponse2 = TraerCliente(Email);
                JObject jResponse2 = JObject.Parse(sResponse2);
                jResponse.Add("Cliente", jResponse2["Cliente"].ToString());
                sResponse = jResponse.ToString();
            }
            catch (Exception Ex)
            {
            }
            return JArray.Parse(sResponse);
        }
        public JArray TraerValidarStock(String Empresa, String Periodo, String CadArticulos)
        {
            String sMensajeError = "";
            //string sResponse = Sql.TraerProcAlm("sp_PedWebPAmigaValidarStock", new String[] { "Empresa", "Periodo", "String" },
            //    new String[] { Empresa, Periodo, CadArticulos });
            string sResponse = Sql.TraerProcAlm(Metodos.TraerValidarStock.proc, new String[] { "Empresa", "Periodo", "String" },
                new String[] { Empresa, Periodo, CadArticulos });
            try
            {
                JObject jResponse = JObject.Parse(sResponse);
                sResponse = jResponse.ToString();
            }
            catch (Exception Ex)
            {
            }
            return JArray.Parse(sResponse);
        }

        public JArray RecibePedidosCancelados(String Empresa, String Periodo, String RFC, String FechaCancelacion,
            String PedidosCancelados)
        {
            String sMensajeError = "";
            //string sResponse = Sql.TraerProcAlm("sp_PedWebPAmigaEnviaRecibePedidosCancelados", new String[] { "Empresa", "Periodo", "RFC", "FechaCancelacion", "StringPedidosCancelados" },
            //    new String[] { Empresa, Periodo, RFC, FechaCancelacion, PedidosCancelados});
            string sResponse = Sql.TraerProcAlm(Metodos.RecibePedidosCancelados.proc, new String[] { "Empresa", "Periodo", "RFC", "FechaCancelacion", "StringPedidosCancelados" },
                 new String[] { Empresa, Periodo, RFC, FechaCancelacion, PedidosCancelados });
            try
            {
                JObject jResponse = JObject.Parse(sResponse);
                sResponse = jResponse.ToString();
            }
            catch (Exception Ex)
            {
            }
            return JArray.Parse(sResponse);
        }

        public JArray ConsultaEstatusPedidos(String Empresa, String Periodo, String ListaPedidos, String CadenaPedidos)
        {
            String sMensajeError = "";
            //string sResponse = Sql.TraerProcAlm("sp_PedWebPAmigaEnviaRecibePedidosCancelados", new String[] { "Empresa", "Periodo", "RFC", "FechaCancelacion", "StringPedidosCancelados" },
            //    new String[] { Empresa, Periodo, RFC, FechaCancelacion, PedidosCancelados});
            string sResponse = Sql.TraerProcAlm(Metodos.ConsultaEstatusPedidos.proc, new String[] { "Empresa", "Periodo", "ListaPedidos", "CadenaPedidos" },
                 new String[] { Empresa, Periodo, ListaPedidos, CadenaPedidos });
            try
            {
                JObject jResponse = JObject.Parse(sResponse);
                sResponse = jResponse.ToString();
            }
            catch (Exception Ex)
            {
            }
            return JArray.Parse(sResponse);
        }


        public String TraerCliente(String MailCliente)
        {
            String sMensajeError = "";
            //string sql = "select Cliente,RazonSocial,CIFDNI,TipoIVA,Moneda,Calle,Colonia,CodPostal,Poblacion,Provincia,Pais,NombreLocal,Tlf1,Email,Entidad,Sucursal,Cuenta,DC,Empresa from vTraerCliente";
            string sql = "select Cliente,RazonSocial,CIFDNI,TipoIVA,Moneda,Calle,Colonia,CodPostal,Poblacion,Provincia,Pais,NombreLocal,Tlf1,Email,Entidad,Sucursal,Cuenta,DC,Empresa from " + Metodos.TraerCliente.proc;
            string sResponse = Sql.TraeValores(sql, new String[] { "Empresa", "Email" }, new String[] { Sql.IdEmpresa, MailCliente });
            return sResponse;
        }
        public JArray ExisteCliente(String MailCliente)
        {
            String sMensajeError = "";
            //string sql = "select count(*) as rCount from vTraerCliente";
            string sql = "select count(*) as rCount from " + Metodos.ExisteCliente.proc;
            string sResponse = Sql.TraeValores(sql, new String[] { "Empresa", "Email" }, new String[] { Sql.IdEmpresa, MailCliente });

            if (sResponse != "" && sResponse != null && sResponse != "[{\"rCount\":\"0\"}]")
            {
                sResponse = new JObject(new JProperty("response", "true")).ToString();
            }
            else
            {
                sResponse = new JObject(new JProperty("response", "false")).ToString();
            }
            return JArray.Parse(sResponse);
        }
        public JArray TraerSerie(String IdSerie)
        {
            String sMensajeError = "";
            //string sql = "select Serie,Descripcion,ValorActual from vTraerSerieWeb";
            string sql = "select Serie,Descripcion,ValorActual from " + Metodos.TraerSerie.proc;
            string sResponse = Sql.TraeValores(sql, new String[] { "Std_Empresa", "Std_Periodo", "Serie" }, new String[] { Sql.IdEmpresa, DateTime.Now.Year.ToString(), IdSerie });
            return JArray.Parse(sResponse);
        }
        public JArray TraerSeriesTodas()
        {
            String sMensajeError = "";
            //string sql = "select Serie,Descripcion from vTraerSeriesWebTodas";
            string sql = "select Serie,Descripcion from " + Metodos.TraerSeriesTodas.proc;
            string sResponse = Sql.TraeValores(sql, new String[] { "Std_Empresa", "Std_Periodo" }, new String[] { Sql.IdEmpresa, DateTime.Now.Year.ToString() });
            return JArray.Parse(sResponse);
        }
        public JArray EnviarStock(String Articulo, String Almacen, String Ubicacion, String Stock)
        {
            String sMensajeError = "";
            //string Table = "MaeStock";
            string Table = Metodos.EnviarStock.proc;
            string sResponse = Sql.ModificaValores(Table, new String[] { "Stock" }, new String[] { Stock },
                new String[] { "Empresa", "Articulo", "Almacen", "Ubicacion" }, new String[] { Sql.IdEmpresa, Articulo, Almacen, Ubicacion });
            return JArray.Parse(sResponse);
        }
        public JArray TraerStock(String Articulo)
        {
            String sMensajeError = "";
            //string sql = "select Empresa,Articulo,Almacen,Ubicacion,Stock from vTraerStock";
            string sql = "select Empresa,Articulo,Almacen,Ubicacion,Stock from " + Metodos.TraerStock.proc;
            string sResponse = Sql.TraeValores(sql, new String[] { "Empresa", "Articulo" }, new String[] { Sql.IdEmpresa, Articulo });
            return JArray.Parse(sResponse);
        }
        public JArray TraerTarifasTodas()
        {
            String sMensajeError = "";
            //string sql = "select Articulo,PrecioVenta from vTraerTarifasTodas";
            string sql = "select Articulo,PrecioVenta from " + Metodos.TraerTarifasTodas.proc;
            string sResponse = Sql.TraeValores(sql, new String[] { "Empresa" }, new String[] { Sql.IdEmpresa });
            return JArray.Parse(sResponse);
        }
        public JArray TraerTarifa(String Articulo)
        {
            String sMensajeError = "";
            //string sql = "select Articulo,Descripcion,PrecioVenta from vTraerTarifa";
            string sql = "select Articulo,Descripcion,PrecioVenta from " + Metodos.TraerTarifa.proc;
            string sResponse = Sql.TraeValores(sql, new String[] { "Empresa", "Articulo" }, new String[] { Sql.IdEmpresa, Articulo });
            return JArray.Parse(sResponse);
        }
        public JArray TipologiaLinLin(String Codigo, String IdTipologiaArticulo, String Nivel)
        {
            String sMensajeError = "";
            //string sql = "select Nivel,Codigo,Descripcion,TipologiaArticulo from vTraerTipologiaLinLin";
            string sql = "select Nivel,Codigo,Descripcion,TipologiaArticulo from " + Metodos.TraerTipologiaLinLin.proc;
            string sResponse = "";
            String[] Campos = new String[0];
            String[] Valores = new String[0];
            if (IdTipologiaArticulo != "" && IdTipologiaArticulo != null)
            {
                Array.Resize(ref Campos, Campos.Length + 1);
                Array.Resize(ref Valores, Valores.Length + 1);
                Campos[Campos.Length - 1] = "TipologiaArticulo";
                Valores[Valores.Length - 1] = IdTipologiaArticulo;
            }
            if (Codigo != "" && Codigo != null)
            {
                Array.Resize(ref Campos, Campos.Length + 1);
                Array.Resize(ref Valores, Valores.Length + 1);
                Campos[Campos.Length - 1] = "Codigo";
                Valores[Valores.Length - 1] = Codigo;
            }
            if (Nivel != "" && Nivel != null)
            {
                Array.Resize(ref Campos, Campos.Length + 1);
                Array.Resize(ref Valores, Valores.Length + 1);
                Campos[Campos.Length - 1] = "Nivel";
                Valores[Valores.Length - 1] = Nivel;
            }
            if (Campos.Length < 1)
            {
                StringBuilder sb = new StringBuilder();
                StringWriter sw = new StringWriter(sb);
                JsonWriter writer = new JsonTextWriter(sw);
                writer.WriteStartArray();
                writer.WriteStartObject();
                writer.WritePropertyName("response");
                writer.WriteValue("no_result");
                writer.WriteEndObject();
                writer.WriteEndArray();
                return JArray.Parse(writer.ToString());
            }
            sResponse = Sql.TraeValores(sql, Campos, Valores);
            return JArray.Parse(sResponse);
        }
        public JArray TipologiasTodasLinLin()
        {
            String sMensajeError = "";
            //string sql = "select Nivel,Codigo,TipologiaArticulo from vTraerTipologiasTodasLinLin";
            string sql = "select Nivel,Codigo,TipologiaArticulo from " + Metodos.TraerTipologiasTodasLinLin.proc;
            string sResponse = Sql.TraeValores(sql, new String[] { "Empresa" }, new String[] { Sql.IdEmpresa });
            return JArray.Parse(sResponse);
        }
        public JArray TipologiaLin(String IdTipologiaArticulo, String Nivel)
        {
            String sMensajeError = "";
            //string sql = "select TipologiaArticulo,Descripcion,Linea,Nivel,Longitud from vTraerTipologiaLin";
            string sql = "select TipologiaArticulo,Descripcion,Linea,Nivel,Longitud from " + Metodos.TraerTipologiaLin.proc;
            string sResponse = "";
            String[] Campos = new String[0];
            String[] Valores = new String[0];
            if (IdTipologiaArticulo != "" && IdTipologiaArticulo != null)
            {
                Array.Resize(ref Campos, Campos.Length + 1);
                Array.Resize(ref Valores, Valores.Length + 1);
                Campos[Campos.Length - 1] = "TipologiaArticulo";
                Valores[Valores.Length - 1] = IdTipologiaArticulo;
            }
            if (Nivel != "" && Nivel != null)
            {
                Array.Resize(ref Campos, Campos.Length + 1);
                Array.Resize(ref Valores, Valores.Length + 1);
                Campos[Campos.Length - 1] = "Nivel";
                Valores[Valores.Length - 1] = Nivel;
            }
            if (Campos.Length < 1)
            {
                StringBuilder sb = new StringBuilder();
                StringWriter sw = new StringWriter(sb);
                JsonWriter writer = new JsonTextWriter(sw);
                writer.WriteStartArray();
                writer.WriteStartObject();
                writer.WritePropertyName("response");
                writer.WriteValue("no_result");
                writer.WriteEndObject();
                writer.WriteEndArray();
                return JArray.Parse(writer.ToString());
            }
            sResponse = Sql.TraeValores(sql, Campos, Valores);
            return JArray.Parse(sResponse);
        }
        public JArray TipologiasTodasLin()
        {
            String sMensajeError = "";
            //string sql = "select TipologiaArticulo,Nivel from vTraerTipologiasTodasLin";
            string sql = "select TipologiaArticulo,Nivel from " + Metodos.TraerTipologiasTodasLin.proc;
            string sResponse = Sql.TraeValores(sql, new String[] { "Empresa" }, new String[] { Sql.IdEmpresa });
            return JArray.Parse(sResponse);
        }
        public JArray Tipologia(String IdTipologiaArticulo)
        {
            String sMensajeError = "";
            //string sql = "select TipologiaArticulo,Descripcion,DescripcionIdioma from vTraerTipologia";
            string sql = "select TipologiaArticulo,Descripcion,DescripcionIdioma from " + Metodos.TraerTipologia.proc;
            string sResponse = Sql.TraeValores(sql, new String[] { "Empresa", "TipologiaArticulo" }, new String[] { Sql.IdEmpresa, IdTipologiaArticulo });
            return JArray.Parse(sResponse);
        }
        public JArray TipologiasTodas()
        {
            String sMensajeError = "";
            //string sql = "select TipologiaArticulo from vTraerTipologiasTodas";
            string sql = "select TipologiaArticulo from " + Metodos.TraerTipologiasTodas.proc;
            string sResponse = Sql.TraeValores(sql, new String[] { "Empresa" }, new String[] { Sql.IdEmpresa });
            return JArray.Parse(sResponse);
        }
        public JArray TipoArticulo(String IdTipoArticulo)
        {
            String sMensajeError = "";
            //string sql = "select TipoArticulo,Descripcion,Familia from vTraerTipoArticulo";
            string sql = "select TipoArticulo,Descripcion,Familia from " + Metodos.TraerTipoArticulo.proc;
            string sResponse = Sql.TraeValores(sql, new String[] { "Empresa", "TipoArticulo" }, new String[] { Sql.IdEmpresa, IdTipoArticulo });
            return JArray.Parse(sResponse);
        }
        public JArray TiposArticulosTodos()
        {
            String sMensajeError = "";
            //string sql = "select TipoArticulo,Descripcion from vTraerTiposArticulosTodos";
            string sql = "select TipoArticulo,Descripcion,Familia from " + Metodos.TraerTiposArticulosTodos.proc;
            string sResponse = Sql.TraeValores(sql, new String[] { "Empresa" }, new String[] { Sql.IdEmpresa });
            return JArray.Parse(sResponse);
        }
        public JArray Familia(String IdFamilia)
        {
            String sMensajeError = "";
            //string sql = "select Familia,Descripcion from vTraerFamilia";
            string sql = "select Familia,Descripcion from " + Metodos.TraerFamilia.proc;
            string sResponse = Sql.TraeValores(sql, new String[] { "Empresa", "Familia" }, new String[] { Sql.IdEmpresa, IdFamilia });
            return JArray.Parse(sResponse);
        }
        public JArray FamiliasTodas()
        {
            String sMensajeError = "";
            //string sql = "select Familia,Descripcion from vTraerFamiliasTodas";
            string sql = "select Familia,Descripcion from " + Metodos.TraerFamiliasTodas.proc;
            string sResponse = Sql.TraeValores(sql, new String[] { "Empresa" }, new String[] { Sql.IdEmpresa });
            return JArray.Parse(sResponse);
        }
        public JArray Producto(String IdArticulo)
        {
            String sMensajeError = "";
            //string sql = "select * from vTraerProducto";
            string sql = "select * from " + Metodos.TraerProducto.proc;
            string sResponse = Sql.TraeValores(sql, new String[] { "Empresa", "Articulo" }, new String[] { Sql.IdEmpresa, IdArticulo });
            return JArray.Parse(sResponse);
        }
        public JArray ProdAlmWeb()
        {
            String sMensajeError = "";
            //string sql = "select Articulo from vTraerProdAlmWeb";
            string sql = "select Articulo from " + Metodos.TraerProdAlmWeb.proc;
            string sResponse = Sql.TraeValores(sql, new String[] { "Empresa" }, new String[] { Sql.IdEmpresa });
            return JArray.Parse(sResponse);
        }
        public JArray GetAlmacenes()
        {
            String sMensajeError = "";
            string sql = "select * from VMaeAlmacenes";
            //string sql = "select Almacen from vTraerAlmWebTodos";
            string sResponse = Sql.TraeValores(sql, new String[] { "Empresa" }, new String[] { Sql.IdEmpresa });
            return JArray.Parse(sResponse);
        }
        public JArray GetAlmacen(String Almacen)
        {
            if (Almacen == "")
            {
                StringBuilder sb = new StringBuilder();
                StringWriter sw = new StringWriter(sb);
                JsonWriter writer = new JsonTextWriter(sw);
                writer.WriteStartArray();
                writer.WriteStartObject();
                writer.WritePropertyName("response");
                writer.WriteValue("El id del almacén está vacio");
                writer.WriteEndObject();
                writer.WriteEndArray();
                return JArray.Parse(writer.ToString());
            }
            String sMensajeError = "";
            //string sql = "select * from vTraerAlmWeb";
            string sql = "select * from " + Metodos.TraerAlmacen.proc;
            string sResponse = Sql.TraeValores(sql, new String[] { "Almacen", "Empresa" }, new String[] { Almacen, Sql.IdEmpresa });
            return JArray.Parse(sResponse);
        }

        public JArray EnviarPedidosCompleto(String Maquina, String Usuario, String IdPedido, String FechaPedido,
            String Cliente, String Moneda, String Cambio, String Nombre, String Email, String Calle, String Colonia, String CodPostal,
            String Poblacion, String Provincia, String Pais, String Telefono, String Facturar, String CIFDNI, String RazonSocial,
            String CalleFra, String ColoniaFra, String CodPostalFra, String PoblacionFra, String ProvinciaFra, String PaisFra, String UsoCFDi,
            String TipoTarifa, String DatosPartidas, String MetodoEnvio, String Sucursal, String Cobro_CobradoPor, String Cobro_Estatus,
            String Cobro_FechaHoraAcreditado, String Cobro_idTransaccion, String Cobro_Importe, String Cobro_Comision, String Cobro_ImporteNeto,
            String Cobro_MP_TipoCobro, String Cobro_MP_TipoTarjeta, String Cobro_MP_TipoCuenta, String Cobro_Observaciones, String GiftCard)
        {


            if (GiftCard.Split('|').Length > 2)
            {
                StringBuilder sb = new StringBuilder();
                StringWriter sw = new StringWriter(sb);
                JsonWriter writer = new JsonTextWriter(sw);
                writer.WriteStartArray();
                writer.WriteStartObject();
                writer.WritePropertyName("response");
                writer.WriteValue("El valor recibido en GiftCard no es correcto.");
                writer.WriteEndObject();
                writer.WriteEndArray();
                return JArray.Parse(writer.ToString());
            }




            String sMensajeError = "";

            //string Table = "sp_PedWebPAmigaRecibePedidoCompleto";
            string Table = Metodos.EnviarPedidosCompleto.proc;
            string sResponse = Sql.EjecutaProcAlm(Table, new String[] { "Maquina", "Usuario", "Empresa", "IdPedido", "FechaPedido",
                "Cliente", "Moneda", "Cambio", "Nombre", "Email", "Calle", "Colonia", "CodPostal", "Poblacion", "Provincia", "Pais",
                "Telefono", "Facturar", "CIFDNI", "RazonSocial", "CalleFra", "ColoniaFra", "CodPostalFra", "PoblacionFra",
                "ProvinciaFra", "PaisFra", "UsoCFDi", "TipoTarifa", "DatosPartidas", "MetodoEnvio", "Sucursal", "Cobro_CobradoPor",
                "Cobro_Estatus", "Cobro_FechaHoraAcreditado", "Cobro_idTransaccion", "Cobro_Importe", "Cobro_Comision", "Cobro_ImporteNeto",
                "Cobro_MP_TipoCobro", "Cobro_MP_TipoTarjeta", "Cobro_MP_TipoCuenta", "Cobro_Observaciones" , "GiftCard" },

            new string[]
            {
                Maquina, Usuario, Sql.IdEmpresa,IdPedido, FechaPedido, Cliente, Moneda, Cambio, Nombre, Email, Calle, Colonia, CodPostal, Poblacion, Provincia, Pais, Telefono, Facturar, CIFDNI,
                RazonSocial, CalleFra, ColoniaFra, CodPostalFra, PoblacionFra, ProvinciaFra, PaisFra, UsoCFDi, TipoTarifa, DatosPartidas, MetodoEnvio, Sucursal, Cobro_CobradoPor, Cobro_Estatus, Cobro_FechaHoraAcreditado, Cobro_idTransaccion, Cobro_Importe, Cobro_Comision, Cobro_ImporteNeto, Cobro_MP_TipoCobro, Cobro_MP_TipoTarjeta, Cobro_MP_TipoCuenta, Cobro_Observaciones,GiftCard

            });


            return JArray.Parse(sResponse);
        }

        public JArray TraerMovimientos(String anyo)
        {

            String sMensajeError = "";
            //string sql = "select Articulo from vTraerProdAlmWeb";
            string sql = "select top 50 * from VPDAProArticulosMovimientoUsuario where Periodo = '" + anyo + "' order by Movimiento desc";
            String sResponse = Sql.TraeValoresSimple(sql);
            return JArray.Parse(sResponse);
        }

        public JArray TraerMovimientosFiltro(String anyo, String tipo, String query)
        {
            String sMensajeError = "";
            //string sql = "select Articulo from vTraerProdAlmWeb";
            string sql = "select top 50 * from VPDAProArticulosMovimientoUsuario where " + tipo + " like '%" + query + "%' and Periodo = '" + anyo + "'";
            String sResponse = Sql.TraeValoresSimple(sql);
            return JArray.Parse(sResponse);
        }

        public JArray TraerContadorMovimientos(String Empresa, String Periodo, String Serie, String Formulario, String Tabla, String CampoAContar)
        {
            String sMensajeError = "";
            String[] valores = new String[] { Empresa, Periodo, Serie, Formulario, Tabla, CampoAContar };
            valores = ValidarNulos(valores);
            string sResponse = Sql.TraerProcAlmGen("prc_ges_Contadores", new String[] { "Empresa", "Periodo", "Serie", "Formulario", "Tabla", "CampoAContar" }, valores);
            try
            {
                JObject jResponse = JObject.Parse(sResponse);
                sResponse = jResponse.ToString();
            }
            catch (Exception Ex)
            {
            }
            return JArray.Parse(sResponse);
        }

        //"EXEC prc_PIM_CreaMovimientosCab " +
        //                                "@Empresa='${BasicData.empresa}'," +
        //                                "@Periodo='${BasicData.year}'," +
        //                                "@Movimiento=${BasicData.idMov}," +
        //                                "@FechaMov= '${BasicData.fechaMovTxt}'," +
        //                                "@Usuario='${BasicData.name}'," +
        //                                "@bdcliente='${BasicData.NumUsuario}'," +
        //                                "@AndenLogistica=''," +
        //                                "@OrigenLogistica=''," +
        //                                "@Observaciones='${binding.descMovimiento.text}'," +
        //                                "@EstadoLogistica=0," +
        //                                "@TipoMovimiento='${BasicData.tipoMov}'," +
        //                                "@OrigenMovimiento='1'," +
        //                                "@LugarDestino=''," +
        //                                "@AlmacenOrigen=''," +
        //                                "@AlmacenDestino=''," +
        //                                "@Servido='0'"


        public JArray CrearCabeceraMovimientos(String Empresa, String Periodo, String Movimiento, String FechaMov, String Usuario, String bdcliente, String AndenLogistica, String OrigenLogistica,
            String Observaciones, String EstadoLogistica, String TipoMovimiento, String OrigenMovimiento, String LugarDestino, String AlmacenOrigen, String AlmacenDestino, String Servido)
        {
            String sMensajeError = "";
            String[] valores = new String[] {Empresa, Periodo, Movimiento, FechaMov, Usuario, bdcliente, AndenLogistica, OrigenLogistica,
            Observaciones, EstadoLogistica, TipoMovimiento, OrigenMovimiento, LugarDestino, AlmacenOrigen, AlmacenDestino, Servido };

            valores = ValidarNulos(valores);

            string sResponse = Sql.TraerProcAlm("prc_PIM_CreaMovimientosCab", new String[] { "Empresa", "Periodo", "Movimiento", "FechaMov", "Usuario", "bdcliente", "AndenLogistica", "OrigenLogistica",
            "Observaciones", "EstadoLogistica", "TipoMovimiento", "OrigenMovimiento", "LugarDestino", "AlmacenOrigen", "AlmacenDestino", "Servido"}, valores);
            try
            {
                JObject jResponse = JObject.Parse(sResponse);
                sResponse = jResponse.ToString();
            }
            catch (Exception Ex)
            {
            }
            return JArray.Parse(sResponse);
        }

        //"EXEC prc_PIM_CreaMovimientosLin " +
        //                               "@Empresa='${BasicData.empresa}'," +
        //                               "@Periodo=${BasicData.year}," +
        //                               "@Referencia='${BasicData.RefPadreMov}'," +
        //                               "@Movimiento=${BasicData.idMov}," +
        //                               "@Linea=1," +
        //                               "@FechaMov='${BasicData.fechaMovTxt}'," +
        //                               "@Cantidad='${binding.editCantidad.value}'," +
        //                               "@Articulo='${binding.numArticulo.text}'," +
        //                               "@Descripcion='${binding.descMovimiento.text}'," +
        //                               "@AlmacenOrigen='${orWareHouse}'," +
        //                               "@AlmacenDestino='${desWareHouse}'," +
        //                               "@AndenLogistica=''," +
        //                               "@EstadoLogistica='0'," +
        //                               "@BDCliente=''," +
        //                               "@CDBPedido='${binding.cdbArticulo.text}'," +
        //                               "@LugarDestino='0'," +
        //                               "@CRU=''"

        public JArray CrearLineasMovimientos(String Empresa, String Periodo, String Referencia, String Movimiento, String Linea, String FechaMov, String Cantidad, String Articulo,
            String Descripcion, String AlmacenOrigen, String AlmacenDestino, String AndenLogistica, String EstadoLogistica, String bdcliente, String CDBPedido, String LugarDestino, String CRU)
        {
            String sMensajeError = "";
            String[] valores = new String[] {Empresa, Periodo, Referencia, Movimiento, Linea, FechaMov, Cantidad, Articulo,
            Descripcion, AlmacenOrigen, AlmacenDestino, AndenLogistica, EstadoLogistica, bdcliente, CDBPedido, LugarDestino, CRU };
            valores = ValidarNulos(valores);
            string sResponse = Sql.TraerProcAlm("prc_PIM_CreaMovimientosLin", new String[] { "Empresa", "Periodo", "Referencia", "Movimiento", "Linea", "FechaMov", "Cantidad", "Articulo",
            "Descripcion", "AlmacenOrigen", "AlmacenDestino", "AndenLogistica", "EstadoLogistica", "bdcliente", "CDBPedido", "LugarDestino","CRU"},valores);
            try
            {
                JObject jResponse = JObject.Parse(sResponse);
                sResponse = jResponse.ToString();
            }
            catch (Exception Ex)
            {
            }
            return JArray.Parse(sResponse);
        }

        /*select * from VMaeArticulosImagenes where Articulo like '%${binding.numArticulo.text}%' or CDB like '%${binding.numArticulo.text}%'*/

        public JArray TraerArticulosFiltroArticuloCBD(String Articulo, String CDB)
        {
            if(Articulo == null)
            {
                Articulo = "";
            }
            if(CDB == null)
            {
                CDB = "";
            }
            String sMensajeError = "";
            //string sql = "select Articulo from vTraerProdAlmWeb";
            string sql = "select * from VMaeArticulosImagenes where Articulo like '%"+ Articulo + "%' or CDB like '%" + CDB + "%'";
            String sResponse = Sql.TraeValoresSimple(sql);
            return JArray.Parse(sResponse);
        }

        /*select top 40 * from VMaeArticulosImagenes*/

        public JArray TraerArticulos()
        { 
            String sMensajeError = "";
            //string sql = "select Articulo from vTraerProdAlmWeb";
            string sql = "select top 40 * from VMaeArticulosImagenes";
            String sResponse = Sql.TraeValoresSimpleImg(sql);
            return JArray.Parse(sResponse);
        }

        /*select top 40 * from VMaeArticulosImagenes*/

        public JArray TraerArticulosTipo(String Tipo, String query)
        {
            String sMensajeError = "";
            //string sql = "select Articulo from vTraerProdAlmWeb";
            string sql = "select top 50 * from VMaeArticulosImagenes where "+Tipo+" like '%" + query + "%'";
            String sResponse = Sql.TraeValoresSimple(sql);
            return JArray.Parse(sResponse);
        }

        /*select * from VPDAProArticulosMovimientoUsuario where MovimientoOrigen = '${BasicData.idMov}'*/

        public JArray TraerMovimientoId(String IdMovimiento)
        {
            String sMensajeError = "";
            //string sql = "select Articulo from vTraerProdAlmWeb";
            string sql = "select * from VPDAProArticulosMovimientoUsuario where MovimientoOrigen = '" + IdMovimiento + "'";
            String sResponse = Sql.TraeValoresSimple(sql);
            return JArray.Parse(sResponse);
        }

        /*select * from VPDAProArticulosMovimientoUsuarioLin where Movimiento = '${BasicData.idMov}'*/

        public JArray TraerArticulosMovimientoId(String IdMovimiento)
        {
            String sMensajeError = "";
            //string sql = "select Articulo from vTraerProdAlmWeb";
            string sql = "select * from VPDAProArticulosMovimientoUsuarioLin where Movimiento = '" + IdMovimiento + "'";
            String sResponse = Sql.TraeValoresSimple(sql);
            return JArray.Parse(sResponse);
        }

        /*prc_PDA_EliminaMovimientos " +
                            "@Empresa='${BasicData.empresa}'," +
                            "@BDCliente=''," +
                            "@RefPadre='${BasicData.RefPadreMov}'*/

        public JArray EliminarMovimiento(String Empresa, String bdcliente, String RefPadre)
        {
            String sMensajeError = "";
            String[] valores = new String[] { Empresa, bdcliente, RefPadre };
            valores = ValidarNulos(valores);
            string sResponse = Sql.TraerProcAlm("prc_PDA_EliminaMovimientos", new String[] { "Empresa", "bdcliente", "RefPadre" },valores);
            try
            {
                JObject jResponse = JObject.Parse(sResponse);
                sResponse = jResponse.ToString();
            }
            catch (Exception Ex)
            {
            }
            return JArray.Parse(sResponse);
        }

        /*"prc_PDA_ActualizaMovimientosDatos " +
                        "@BDCliente='', " +
                        "@Descripcion='${binding.descripcionMovView.text.toString()}', " +
                        "@RefPadre='${BasicData.RefPadreMov}', " +
                        "@Servido='${BasicData.Cerrado}'"*/

        public JArray ActualizarMovimiento(String bdcliente, String Descripcion,  String RefPadre, String Servido)
        {
            String sMensajeError = "";
            String[] valores = new String[] { bdcliente, Descripcion, RefPadre, Servido };
            valores = ValidarNulos(valores);
            string sResponse = Sql.TraerProcAlm("prc_PDA_ActualizaMovimientosDatos", new String[] { "bdcliente", "Descripcion", "RefPadre", "Servido" },valores);
            try
            {
                JObject jResponse = JObject.Parse(sResponse);
                sResponse = jResponse.ToString();
            }
            catch (Exception Ex)
            {
            }
            return JArray.Parse(sResponse);
        }

        /*select * from VMaeArticulosImagenes where Articulo = '${binding.numArticulo.text}'*/

        public JArray TraerImagenArticulo(String numArticulo)
        {
            String sMensajeError = "";
            //string sql = "select Articulo from vTraerProdAlmWeb";
            string sql = "select * from VMaeArticulosImagenes where Articulo = '" + numArticulo + "'";
            String sResponse = Sql.TraeValoresSimple(sql);
            return JArray.Parse(sResponse);
        }

        /*select * from VMaeArticulosImagenes where Descripcion like '%"+ Descripcion + "%' '*/

        public JArray TraerArticuloDescripcion(String Descripcion)
        {
            String sMensajeError = "";
            //string sql = "select Articulo from vTraerProdAlmWeb";
            string sql = "select * from VMaeArticulosImagenes where Descripcion = '" + Descripcion + "'";
            String sResponse = Sql.TraeValoresSimple(sql);
            return JArray.Parse(sResponse);
        }

        /*select * from VPDAProArticulosMovimientoUsuarioLin where Movimiento = '${BasicData.idMov}' and Articulo = '${BasicData.idArticuloMov}'*/

        public JArray TraerImagenArticuloMovimientoId(String IdMovimiento, String idArticulo)
        {
            String sMensajeError = "";
            //string sql = "select Articulo from vTraerProdAlmWeb";
            string sql = "select * from VPDAProArticulosMovimientoUsuarioLin where Movimiento = '" + IdMovimiento + "' and Articulo = '" + idArticulo + "'";
            String sResponse = Sql.TraeValoresSimple(sql);
            return JArray.Parse(sResponse);
        }

        /*"EXEC prc_PDA_ActualizaMovimientosDatosLin " +
                                        "@BDCliente=''," +
                                        "@AlmacenOrigen='${orWareHouse}'," +
                                        "@AlmacenDestino='${desWareHouse}'," +
                                        "@Articulo='${binding.numArticulo.text}'," +
                                        "@Descripcion='${binding.descMovimiento.text}'," +
                                        "@Lote=''," +
                                        "@CRU=''," +
                                        "@Cantidad='${binding.editCantidad.value}'," +
                                        "@RefPadre='${BasicData.RefPadreLin}'"*/


        public JArray ActualizarArticuloMovimiento(String BDCliente, String AlmacenOrigen, String AlmacenDestino, String Articulo, String Descripcion, String Lote, String CRU, String Cantidad, String RefPadre)
        {
            String sMensajeError = "";
            String[] valores = new String[] { BDCliente, AlmacenOrigen, AlmacenDestino, Articulo, Descripcion, Lote, CRU, Cantidad, RefPadre };
            valores = ValidarNulos(valores);
            string sResponse = Sql.TraerProcAlm("prc_PDA_ActualizaMovimientosDatosLin", new String[] { "BDCliente", "AlmacenOrigen", "AlmacenDestino", "Articulo", "Descripcion", "Lote"
            , "CRU", "Cantidad", "RefPadre"},valores);
            try
            {
                JObject jResponse = JObject.Parse(sResponse);
                sResponse = jResponse.ToString();
            }
            catch (Exception Ex)
            {
            }
            return JArray.Parse(sResponse);
        }

        /*"prc_PDA_EliminaMovimientosLin " +
                                    "@Empresa='${BasicData.empresa}'," +
                                    "@BDCliente=''," +
                                    "@Movimiento='${BasicData.idMov}'," +
                                    "@RefPadre='${BasicData.RefPadreLin}'"*/

        public JArray EliminarArticuloMovimiento(String Empresa, String BDCliente, String Movimiento, String RefPadre)
        {
            String sMensajeError = "";
            String[] valores = new String[] { Empresa, BDCliente, Movimiento, RefPadre };
            valores = ValidarNulos(valores);
            string sResponse = Sql.TraerProcAlm("prc_PDA_EliminaMovimientosLin", new String[] { "Empresa", "BDCliente", "Movimiento", "RefPadre" },valores);
            try
            {
                JObject jResponse = JObject.Parse(sResponse);
                sResponse = jResponse.ToString();
            }
            catch (Exception Ex)
            {
            }
            return JArray.Parse(sResponse);
        }


        public JArray TraerInfoUsuario(String NombrePersona, String Contrasenya)
        {
            String sMensajeError = "";
            //string sql = "select Articulo from vTraerProdAlmWeb";
            string sql = "select * from VGen_Usuarios where Nombre = '" + NombrePersona + "' and Password = '" + Contrasenya + "'";
            String sResponse = Sql.TraeValoresSimple(sql,"Gen");
            return JArray.Parse(sResponse);
        }

        public String[] ValidarNulos(String[] valores)
        {
            for (int i = 0; i < valores.Length; i++)
            {
                if (valores[i] == null)
                {
                    valores[i] = "";
                }
            }
            return valores;
        }
        //// POST api/values
        //public void Post([FromBody]string value)
        //{
        //    //value = "ejemplo";
        //}

        //// PUT api/values/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/values/5
        //public void Delete(int id)
        //{
        //}
    }
}
