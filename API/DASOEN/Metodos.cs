using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace DASOEN
{
    public static class Metodos
    {
        public readonly static string Namespace =
        ConfigurationManager.AppSettings["DefaulIP"];
        public static cConfig TraerAlmacenesTodos
        {
            get
            {
                return new cConfig()
                {
                    method = ConfigurationManager.AppSettings["TraerAlmacenesTodos"].Split('|')[0],
                    proc = ConfigurationManager.AppSettings["TraerAlmacenesTodos"].Split('|')[1]
                };
            }
        }
        public static cConfig TraerAlmacen
        {
            get
            {
                return new cConfig()
                {
                    method = ConfigurationManager.AppSettings["TraerAlmacen"].Split('|')[0],
                    proc = ConfigurationManager.AppSettings["TraerAlmacen"].Split('|')[1]
                };
            }
        }
        public static cConfig TraerProdAlmWeb
        {
            get
            {
                return new cConfig()
                {
                    method = ConfigurationManager.AppSettings["TraerProdAlmWeb"].Split('|')[0],
                    proc = ConfigurationManager.AppSettings["TraerProdAlmWeb"].Split('|')[1]
                };
            }
        }
        public static cConfig TraerDetallesArt
        {
            get
            {
                return new cConfig()
                {
                    method = ConfigurationManager.AppSettings["TraerDetallesArt"].Split('|')[0],
                    proc = ConfigurationManager.AppSettings["TraerDetallesArt"].Split('|')[1]
                };
            }
        }
        public static cConfig TraerProducto
        {
            get
            {
                return new cConfig()
                {
                    method = ConfigurationManager.AppSettings["TraerProducto"].Split('|')[0],
                    proc = ConfigurationManager.AppSettings["TraerProducto"].Split('|')[1]
                };
            }
        }
        public static cConfig TraerFamiliasTodas
        {
            get
            {
                return new cConfig()
                {
                    method = ConfigurationManager.AppSettings["TraerFamiliasTodas"].Split('|')[0],
                    proc = ConfigurationManager.AppSettings["TraerFamiliasTodas"].Split('|')[1]
                };
            }
        }
        public static cConfig TraerFamilia
        {
            get
            {
                return new cConfig()
                {
                    method = ConfigurationManager.AppSettings["TraerFamilia"].Split('|')[0],
                    proc = ConfigurationManager.AppSettings["TraerFamilia"].Split('|')[1]
                };
            }
        }
        public static cConfig TraerTiposArticulosTodos
        {
            get
            {
                return new cConfig()
                {
                    method = ConfigurationManager.AppSettings["TraerTiposArticulosTodos"].Split('|')[0],
                    proc = ConfigurationManager.AppSettings["TraerTiposArticulosTodos"].Split('|')[1]
                };
            }
        }
        public static cConfig TraerTipoArticulo
        {
            get
            {
                return new cConfig()
                {
                    method = ConfigurationManager.AppSettings["TraerTipoArticulo"].Split('|')[0],
                    proc = ConfigurationManager.AppSettings["TraerTipoArticulo"].Split('|')[1]
                };
            }
        }
        public static cConfig TraerTipologiasTodas
        {
            get
            {
                return new cConfig()
                {
                    method = ConfigurationManager.AppSettings["TraerTipologiasTodas"].Split('|')[0],
                    proc = ConfigurationManager.AppSettings["TraerTipologiasTodas"].Split('|')[1]
                };
            }
        }
        public static cConfig TraerTipologia
        {
            get
            {
                return new cConfig()
                {
                    method = ConfigurationManager.AppSettings["TraerTipologia"].Split('|')[0],
                    proc = ConfigurationManager.AppSettings["TraerTipologia"].Split('|')[1]
                };
            }
        }
        public static cConfig TraerTipologiasTodasLin
        {
            get
            {
                return new cConfig()
                {
                    method = ConfigurationManager.AppSettings["TraerTipologiasTodasLin"].Split('|')[0],
                    proc = ConfigurationManager.AppSettings["TraerTipologiasTodasLin"].Split('|')[1]
                };
            }
        }
        public static cConfig TraerTipologiaLin
        {
            get
            {
                return new cConfig()
                {
                    method = ConfigurationManager.AppSettings["TraerTipologiaLin"].Split('|')[0],
                    proc = ConfigurationManager.AppSettings["TraerTipologiaLin"].Split('|')[1]
                };
            }
        }
        public static cConfig TraerTipologiasTodasLinLin
        {
            get
            {
                return new cConfig()
                {
                    method = ConfigurationManager.AppSettings["TraerTipologiasTodasLinLin"].Split('|')[0],
                    proc = ConfigurationManager.AppSettings["TraerTipologiasTodasLinLin"].Split('|')[1]
                };
            }
        }
        public static cConfig TraerTipologiaLinLin
        {
            get
            {
                return new cConfig()
                {
                    method = ConfigurationManager.AppSettings["TraerTipologiaLinLin"].Split('|')[0],
                    proc = ConfigurationManager.AppSettings["TraerTipologiaLinLin"].Split('|')[1]
                };
            }
        }
        public static cConfig TraerTarifasTodas
        {
            get
            {
                return new cConfig()
                {
                    method = ConfigurationManager.AppSettings["TraerTarifasTodas"].Split('|')[0],
                    proc = ConfigurationManager.AppSettings["TraerTarifasTodas"].Split('|')[1]
                };
            }
        }
        public static cConfig TraerTarifa
        {
            get
            {
                return new cConfig()
                {
                    method = ConfigurationManager.AppSettings["TraerTarifa"].Split('|')[0],
                    proc = ConfigurationManager.AppSettings["TraerTarifa"].Split('|')[1]
                };
            }
        }
        public static cConfig TraerSerie
        {
            get
            {
                return new cConfig()
                {
                    method = ConfigurationManager.AppSettings["TraerSerie"].Split('|')[0],
                    proc = ConfigurationManager.AppSettings["TraerSerie"].Split('|')[1]
                };
            }
        }
        public static cConfig TraerSeriesTodas
        {
            get
            {
                return new cConfig()
                {
                    method = ConfigurationManager.AppSettings["TraerSeriesTodas"].Split('|')[0],
                    proc = ConfigurationManager.AppSettings["TraerSeriesTodas"].Split('|')[1]
                };
            }
        }
        public static cConfig EnviarPedido
        {
            get
            {
                return new cConfig()
                {
                    method = ConfigurationManager.AppSettings["EnviarPedido"].Split('|')[0],
                    proc = ConfigurationManager.AppSettings["EnviarPedido"].Split('|')[1]
                };
            }
        }
        public static cConfig TraerPedido
        {
            get
            {
                return new cConfig()
                {
                    method = ConfigurationManager.AppSettings["TraerPedido"].Split('|')[0],
                    proc = ConfigurationManager.AppSettings["TraerPedido"].Split('|')[1]
                };
            }
        }
        public static cConfig TraerTraking
        {
            get
            {
                return new cConfig()
                {
                    method = ConfigurationManager.AppSettings["TraerTraking"].Split('|')[0],
                    proc = ConfigurationManager.AppSettings["TraerTraking"].Split('|')[1]
                };
            }
        }
        public static cConfig TraerImagen
        {
            get
            {
                return new cConfig()
                {
                    method = ConfigurationManager.AppSettings["TraerImagen"].Split('|')[0],
                    proc = ConfigurationManager.AppSettings["TraerImagen"].Split('|')[1]
                };
            }
        }
        public static cConfig TraerMetodosPago
        {
            get
            {
                return new cConfig()
                {
                    method = ConfigurationManager.AppSettings["TraerMetodosPago"].Split('|')[0],
                    proc = ConfigurationManager.AppSettings["TraerMetodosPago"].Split('|')[1]
                };
            }
        }
        public static cConfig ExisteCliente
        {
            get
            {
                return new cConfig()
                {
                    method = ConfigurationManager.AppSettings["ExisteCliente"].Split('|')[0],
                    proc = ConfigurationManager.AppSettings["ExisteCliente"].Split('|')[1]
                };
            }
        }
        public static cConfig TraerCliente
        {
            get
            {
                return new cConfig()
                {
                    method = ConfigurationManager.AppSettings["TraerCliente"].Split('|')[0],
                    proc = ConfigurationManager.AppSettings["TraerCliente"].Split('|')[1]
                };
            }
        }
        public static cConfig AltaCliente
        {
            get
            {
                return new cConfig()
                {
                    method = ConfigurationManager.AppSettings["AltaCliente"].Split('|')[0],
                    proc = ConfigurationManager.AppSettings["AltaCliente"].Split('|')[1]
                };
            }
        }
        public static cConfig TraerMonedas
        {
            get
            {
                return new cConfig()
                {
                    method = ConfigurationManager.AppSettings["TraerMonedas"].Split('|')[0],
                    proc = ConfigurationManager.AppSettings["TraerMonedas"].Split('|')[1]
                };
            }
        }
        public static cConfig TraerMoneda
        {
            get
            {
                return new cConfig()
                {
                    method = ConfigurationManager.AppSettings["TraerMoneda"].Split('|')[0],
                    proc = ConfigurationManager.AppSettings["TraerMoneda"].Split('|')[1]
                };
            }
        }
        public static cConfig GenerarDevolucion
        {
            get
            {
                return new cConfig()
                {
                    method = ConfigurationManager.AppSettings["GenerarDevolucion"].Split('|')[0],
                    proc = ConfigurationManager.AppSettings["GenerarDevolucion"].Split('|')[1]
                };
            }
        }
        public static cConfig EnviarStock
        {
            get
            {
                return new cConfig()
                {
                    method = ConfigurationManager.AppSettings["EnviarStock"].Split('|')[0],
                    proc = ConfigurationManager.AppSettings["EnviarStock"].Split('|')[1]
                };
            }
        }
        public static cConfig TraerStock
        {
            get
            {
                return new cConfig()
                {
                    method = ConfigurationManager.AppSettings["TraerStock"].Split('|')[0],
                    proc = ConfigurationManager.AppSettings["TraerStock"].Split('|')[1]
                };
            }
        }
        public static cConfig TraerAtributosArt
        {
            get
            {
                return new cConfig()
                {
                    method = ConfigurationManager.AppSettings["TraerAtributosArt"].Split('|')[0],
                    proc = ConfigurationManager.AppSettings["TraerAtributosArt"].Split('|')[1]
                };
            }
        }
        public static cConfig TraerAtributosLineas
        {
            get
            {
                return new cConfig()
                {
                    method = ConfigurationManager.AppSettings["TraerAtributosLineas"].Split('|')[0],
                    proc = ConfigurationManager.AppSettings["TraerAtributosLineas"].Split('|')[1]
                };
            }
        }
        public static cConfig TraerArticulosColecciones
        {
            get
            {
                return new cConfig()
                {
                    method = ConfigurationManager.AppSettings["TraerArticulosColecciones"].Split('|')[0],
                    proc = ConfigurationManager.AppSettings["TraerArticulosColecciones"].Split('|')[1]
                };
            }
        }
        public static cConfig TraerArticulosTam
        {
            get
            {
                return new cConfig()
                {
                    method = ConfigurationManager.AppSettings["TraerArticulosTam"].Split('|')[0],
                    proc = ConfigurationManager.AppSettings["TraerArticulosTam"].Split('|')[1]
                };
            }
        }
        public static cConfig TraerValidarStock
        {
            get
            {
                return new cConfig()
                {
                    method = ConfigurationManager.AppSettings["TraerValidarStock"].Split('|')[0],
                    proc = ConfigurationManager.AppSettings["TraerValidarStock"].Split('|')[1]
                };
            }
        }
        public static cConfig EnviarPedidosCompleto
        {
            get
            {
                return new cConfig()
                {
                    method = ConfigurationManager.AppSettings["EnviarPedidosCompleto"].Split('|')[0],
                    proc = ConfigurationManager.AppSettings["EnviarPedidosCompleto"].Split('|')[1]
                };
            }
        }

        public static cConfig RecibePedidosCancelados
        {
            get
            {
                return new cConfig()
                {
                    method = ConfigurationManager.AppSettings["RecibePedidosCancelados"].Split('|')[0],
                    proc = ConfigurationManager.AppSettings["RecibePedidosCancelados"].Split('|')[1]
                };
            }
        }

        public static cConfig ConsultaEstatusPedidos
        {
            get
            {
                return new cConfig()
                {
                    method = ConfigurationManager.AppSettings["ConsultaEstatusPedidos"].Split('|')[0],
                    proc = ConfigurationManager.AppSettings["ConsultaEstatusPedidos"].Split('|')[1]
                };
            }
        }

        public static cConfig TraerMovimientos
        {
            get
            {
                return new cConfig()
                {
                    method = ConfigurationManager.AppSettings["TraerMovimientos"].Split('|')[0],
                    proc = ConfigurationManager.AppSettings["TraerMovimientos"].Split('|')[1]
                };
            }
        }

        public static cConfig TraerMovimientosFiltro
        {
            get
            {
                return new cConfig()
                {
                    method = ConfigurationManager.AppSettings["TraerMovimientosFiltro"].Split('|')[0],
                    proc = ConfigurationManager.AppSettings["TraerMovimientosFiltro"].Split('|')[1]
                };
            }
        }

        public static cConfig TraerContadorMovimientos
        {
            get
            {
                return new cConfig()
                {
                    method = ConfigurationManager.AppSettings["TraerContadorMovimientos"].Split('|')[0],
                    proc = ConfigurationManager.AppSettings["TraerContadorMovimientos"].Split('|')[1]
                };
            }
        }

        /*NUEVOS METODOS 24/05/2022*/

        public static cConfig CrearCabeceraMovimientos
        {
            get
            {
                return new cConfig()
                {
                    method = ConfigurationManager.AppSettings["CrearCabeceraMovimientos"].Split('|')[0],
                    proc = ConfigurationManager.AppSettings["CrearCabeceraMovimientos"].Split('|')[1]
                };
            }
        }

        public static cConfig CrearLineasMovimientos
        {
            get
            {
                return new cConfig()
                {
                    method = ConfigurationManager.AppSettings["CrearLineasMovimientos"].Split('|')[0],
                    proc = ConfigurationManager.AppSettings["CrearLineasMovimientos"].Split('|')[1]
                };
            }
        }

        public static cConfig TraerArticulosFiltroArticuloCBD
        {
            get
            {
                return new cConfig()
                {
                    method = ConfigurationManager.AppSettings["TraerArticulosFiltroArticuloCBD"].Split('|')[0],
                    proc = ConfigurationManager.AppSettings["TraerArticulosFiltroArticuloCBD"].Split('|')[1]
                };
            }
        }

        public static cConfig TraerArticulos
        {
            get
            {
                return new cConfig()
                {
                    method = ConfigurationManager.AppSettings["TraerArticulos"].Split('|')[0],
                    proc = ConfigurationManager.AppSettings["TraerArticulos"].Split('|')[1]
                };
            }
        }

        public static cConfig TraerArticulosTipo
        {
            get
            {
                return new cConfig()
                {
                    method = ConfigurationManager.AppSettings["TraerArticulosTipo"].Split('|')[0],
                    proc = ConfigurationManager.AppSettings["TraerArticulosTipo"].Split('|')[1]
                };
            }
        }

        public static cConfig TraerMovimientoId
        {
            get
            {
                return new cConfig()
                {
                    method = ConfigurationManager.AppSettings["TraerMovimientoId"].Split('|')[0],
                    proc = ConfigurationManager.AppSettings["TraerMovimientoId"].Split('|')[1]
                };
            }
        }

        public static cConfig TraerArticulosMovimientoId
        {
            get
            {
                return new cConfig()
                {
                    method = ConfigurationManager.AppSettings["TraerArticulosMovimientoId"].Split('|')[0],
                    proc = ConfigurationManager.AppSettings["TraerArticulosMovimientoId"].Split('|')[1]
                };
            }
        }

        public static cConfig EliminarMovimiento
        {
            get
            {
                return new cConfig()
                {
                    method = ConfigurationManager.AppSettings["EliminarMovimiento"].Split('|')[0],
                    proc = ConfigurationManager.AppSettings["EliminarMovimiento"].Split('|')[1]
                };
            }
        }

        public static cConfig ActualizarMovimiento
        {
            get
            {
                return new cConfig()
                {
                    method = ConfigurationManager.AppSettings["ActualizarMovimiento"].Split('|')[0],
                    proc = ConfigurationManager.AppSettings["ActualizarMovimiento"].Split('|')[1]
                };
            }
        }

        public static cConfig TraerImagenArticulo
        {
            get
            {
                return new cConfig()
                {
                    method = ConfigurationManager.AppSettings["TraerImagenArticulo"].Split('|')[0],
                    proc = ConfigurationManager.AppSettings["TraerImagenArticulo"].Split('|')[1]
                };
            }
        }

        public static cConfig TraerImagenArticuloMovimientoId
        {
            get
            {
                return new cConfig()
                {
                    method = ConfigurationManager.AppSettings["TraerImagenArticuloMovimientoId"].Split('|')[0],
                    proc = ConfigurationManager.AppSettings["TraerImagenArticuloMovimientoId"].Split('|')[1]
                };
            }
        }

        public static cConfig ActualizarArticuloMovimiento
        {
            get
            {
                return new cConfig()
                {
                    method = ConfigurationManager.AppSettings["ActualizarArticuloMovimiento"].Split('|')[0],
                    proc = ConfigurationManager.AppSettings["ActualizarArticuloMovimiento"].Split('|')[1]
                };
            }
        }

        public static cConfig EliminarArticuloMovimiento
        {
            get
            {
                return new cConfig()
                {
                    method = ConfigurationManager.AppSettings["EliminarArticuloMovimiento"].Split('|')[0],
                    proc = ConfigurationManager.AppSettings["EliminarArticuloMovimiento"].Split('|')[1]
                };
            }
        }

        public static cConfig TraerArticuloDescripcion
        {
            get
            {
                return new cConfig()
                {
                    method = ConfigurationManager.AppSettings["TraerArticuloDescripcion"].Split('|')[0],
                    proc = ConfigurationManager.AppSettings["TraerArticuloDescripcion"].Split('|')[1]
                };
            }
        }

        public static cConfig TraerInfoUsuario
        {
            get
            {
                return new cConfig()
                {
                    method = ConfigurationManager.AppSettings["TraerInfoUsuario"].Split('|')[0],
                    proc = ConfigurationManager.AppSettings["TraerInfoUsuario"].Split('|')[1]
                };
            }
        }

        public class cConfig
        {
            public String method { get; set; }
            public String proc { get; set; }
        }
    }
}
