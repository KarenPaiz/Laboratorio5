using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Laboratorio_5_diccionarios.Models;
using System.IO;

namespace Laboratorio_5_diccionarios.Controllers
{
    public class EstampasController : Controller
    {

        private static Dictionary<string, Guardado> DiccionarioAlbum;
        private static Dictionary<string, Guardado> DiccionarioMisEstampas = new Dictionary<string, Guardado>();
        public static List<Estampas> ListaAux;
        // GET: Estampas
        public ActionResult Index()
        {
            return View();
        }
        Estampas aux;
        public void leerArchivoEstampas()
        {
            string Path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/Estampas.csv";
            Estampas aux;
            using (StreamReader sr = System.IO.File.OpenText(Path))
            {
                string s;
                while ((s = sr.ReadLine()) != null)
                {
                    if (s != "")
                    {
                        aux = new Estampas();
                        aux.ID = Convert.ToInt32(s.Split('|')[0]);
                        aux.NombreJ = s.Split('|')[1];
                        aux.Equipo = s.Split('|')[2];
                        int A;
                        A = Convert.ToInt16( s.Split('|')[3]);
                        if (A == 1)
                            aux.Especial = true;
                        else
                            aux.Especial = false;
                        aux.Cantidad = Convert.ToInt16( s.Split('|')[3]);
                        Guardado Gaux = new Guardado();
                        if (DiccionarioAlbum.ContainsKey(aux.Equipo) == true)
                        {
                            DiccionarioAlbum[aux.Equipo].ListaEstampas.Add(aux);
                        }
                        else
                        {
                            Gaux.ListaEstampas.Add(aux);
                            DiccionarioAlbum.Add(aux.Equipo, Gaux);
                        }
                        
                    }
                }


            }
        }      //YA
        public void leerArchivoEstampasD()
        {
            string Path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/EstampasDisponibles.csv";
            Estampas aux;
            using (StreamReader sr = System.IO.File.OpenText(Path))
            {
                string s;
                while ((s = sr.ReadLine()) != null)
                {
                    if (s != "")
                    {
                        aux = new Estampas();
                        aux.ID = Convert.ToInt32(s.Split('|')[0]);
                        aux.NombreJ = s.Split('|')[1];
                        aux.Equipo = s.Split('|')[2];
                        aux.Cantidad = Convert.ToInt16(s.Split('|')[3]);
                        Guardado Gaux = new Guardado();
                        if (DiccionarioMisEstampas.ContainsKey(aux.Equipo) == true)
                        {
                            DiccionarioMisEstampas[aux.Equipo].ListaEstampas.Add(aux);
                        }
                        else
                        {
                            Gaux.ListaEstampas.Add(aux);
                            DiccionarioMisEstampas.Add(aux.Equipo, Gaux);
                        }

                    }
                }


            }
        }      //YA
     
        public ActionResult VerAlbum()
        {
            if (DiccionarioAlbum == null)
            {
                DiccionarioAlbum = new Dictionary<string, Guardado>();
                leerArchivoEstampas();
                leerArchivoEstampasD();
                ListaAux = new List<Estampas>();
                foreach (var A in DiccionarioAlbum.Values)
                {
                    foreach (var B in A.ListaEstampas)
                    {
                        ListaAux.Add(B);
                    }
                }
            }
            ViewBag.Elementos = ListaAux;
            return View();
        }

        public ActionResult VerDisponibles()
        {
            ViewBag.Elementos = ListaAux;
            return View();
        }
        public ActionResult VerNoDisponibles()
        {
            ViewBag.Elementos = ListaAux;
            return View();
        }
        public ActionResult EstampasEspeciales()
        {
            if (DiccionarioAlbum == null)
            {
                DiccionarioAlbum = new Dictionary<string, Guardado>();
                leerArchivoEstampas();
                leerArchivoEstampasD();
                ListaAux = new List<Estampas>();
                foreach (var A in DiccionarioAlbum.Values)
                {
                    foreach (var B in A.ListaEstampas)
                    {
                        ListaAux.Add(B);
                    }
                }
            }
            ViewBag.Elementos = ListaAux;
            return View();
        }

        public ActionResult Equipos()
        {
            try { 
            ViewBag.Elementos = DiccionarioAlbum[Request.Form["Equipo"]].ListaEstampas;
            }
            catch { }
            return View();
        }      //YA
        public ActionResult Equipos2()
        {
            if (DiccionarioAlbum == null)
            {
                DiccionarioAlbum = new Dictionary<string, Guardado>();
                leerArchivoEstampas();
                leerArchivoEstampasD();
            }

            return View();
        }     //YA

        public ActionResult Actualizar()
        {
            bool SePudo = true;
            try
            {
                List<Estampas> ListAux = DiccionarioAlbum[Request.Form["Equipo"]].ListaEstampas.ToList();
                foreach (var A in ListAux)
                {
                    if (A.ID == Convert.ToInt16(Request.Form["número"]))
                    {
                        A.Cantidad++;
                        bool bandera = false;
                        if (DiccionarioMisEstampas.ContainsKey(Request.Form["Equipo"]) == true)
                        {
                            foreach (var B in DiccionarioMisEstampas[Request.Form["Equipo"]].ListaEstampas)
                            {
                                if (B.ID == Convert.ToInt16(Request.Form["número"]))
                                {
                                    bandera = true;
                                }
                            }
                        }
                        if (bandera == true)
                        {
                            DiccionarioMisEstampas[Request.Form["Equipo"]].ListaEstampas.Add(A);
                        }
                    }
                }
            }
             catch
            {
                SePudo = false;
            }
            ViewBag.Elemento = SePudo;
            return View();
        }      //YA
        public ActionResult Actualizar2()
        {
            return View();
        }     //YA
    }
}