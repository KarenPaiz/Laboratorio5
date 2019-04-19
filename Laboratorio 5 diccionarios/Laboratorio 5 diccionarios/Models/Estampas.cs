using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Laboratorio_5_diccionarios.Models
{
    public class Estampas
    {
        public int ID { get; set; }
        public string NombreJ { get; set; }
        public bool Especial { get; set; }
        public string Equipo { get; set; }
        public int Cantidad { get; set; }
    }
    public class Guardado
    {
        public List<Estampas> ListaEstampas;
        public Guardado()
        {
            ListaEstampas = new List<Estampas>();
        }
    }
}