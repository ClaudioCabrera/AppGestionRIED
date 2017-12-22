using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Collections.Specialized;

namespace AppGestionRIED.serviciosRemotos
{
    public class CServiciosRemotos
    {
        //public String servidor;

        public string obtenerDatosElemento(string codigo_barra)
        {
            WebClient cliente = new WebClient();
             Uri uri = new Uri("http://sonrais.com/activos_fijos/servicio_consulta_activo.php");
           // Uri uri = new Uri("http://104.131.11.253/activosfijos/servicio_consulta_activo.php");
            NameValueCollection parametros = new NameValueCollection();
            parametros.Add("p_codigo_barra", codigo_barra);

            byte[] responseBytes = cliente.UploadValues(uri, "POST", parametros);
            string cadenaRespuesta = Encoding.UTF8.GetString(responseBytes);
            return cadenaRespuesta;
        }

     


        public string agregarComentario(string idcomentario,string comentario,int rut_usuario,string activo)//string estado_fisico,
        {
            WebClient clienteWeb = new WebClient();
            // Uri uri = new Uri("http://portal.unap.cl/kb/aula_virtual/serviciosremotos/registro-comentario-activo.php");
            Uri uri = new Uri("http://sonrais.com/activos_fijos/registro-comentario-activo.php");
            NameValueCollection parametros = new NameValueCollection();
           // parametros.Add("p_id_comentario", "20171125143000031151");
            parametros.Add("p_id_comentario", idcomentario);
           // parametros.Add("p_comentario", "dsadas");
            parametros.Add("p_comentario", comentario);
          //  parametros.Add("p_usuario", "444444444");
            parametros.Add("p_usuario", rut_usuario.ToString());
           /// parametros.Add("p_estado", "B");
           // parametros.Add("p_estado", estado_fisico);
            //parametros.Add("p_activo", "031151");
            parametros.Add("p_activo", activo);
            Byte[] respuestaByte = clienteWeb.UploadValues(uri, "POST", parametros);
            string respuestaString = Encoding.UTF8.GetString(respuestaByte);

            return respuestaString;
        }

        public string obtenerReportes(string codigo_barra)
        {
            WebClient cliente = new WebClient();
            Uri uri = new Uri("http://sonrais.com/activos_fijos/servicio-consulta-reportes.php");
            NameValueCollection parametros = new NameValueCollection();
            parametros.Add("p_codigo_barra", codigo_barra);
            byte[] responseBytes = cliente.UploadValues(uri, "POST", parametros);
            string cadenaRespuesta = Encoding.UTF8.GetString(responseBytes);
            return cadenaRespuesta;

            return cadenaRespuesta;
        }

        public string enviarImagenServidor(string nombreFoto, string rutaCompleta)
        {
            WebClient cliente = new WebClient();
          //  Uri uri = new Uri("http://sonrais.com/activos_fijos/app_fotos.php?nombre=" + nombreFoto);
            Uri uri = new Uri("http://jupiter.unap.cl/SV2/public/mobile-unico/registrar-reporte-imagen.php");
            cliente.Headers.Add("Content-Type", "binary/octet-stream");
            byte[] result = cliente.UploadFile(uri, "POST", rutaCompleta);
            string s = System.Text.Encoding.UTF8.GetString(result, 0, result.Length);
            return s;
        }

        internal string validarCredenciales(int rut, string clave)
        {
            WebClient cliente = new WebClient();
            Uri uri = new Uri("http://www.unap.cl/campus_online/apps/app_mobile/presentacion/verifica_acceso.php");
            NameValueCollection parametros = new NameValueCollection();
            parametros.Add("p_rut", rut.ToString());
            parametros.Add("p_clave", clave);
            parametros.Add("p_tid", "1");
            parametros.Add("p_bandera", "1");

            byte[] responseBytes = cliente.UploadValues(uri, "POST", parametros);
            string responseString = Encoding.UTF8.GetString(responseBytes);
            return responseString;
        }

        public string cambiarEstado(string pick_estado_fisico_final)
        {
            WebClient cliente = new WebClient();
            Uri uri = new Uri("");
            NameValueCollection parametros = new NameValueCollection();
            parametros.Add("p_estado",pick_estado_fisico_final);

            byte[] responseBytes = cliente.UploadValues(uri, "POST", parametros);
            string responseString = Encoding.UTF8.GetString(responseBytes);
            return responseString;

        }
    }
}
