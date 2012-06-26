using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AltaMontanha.Models.Dominio
{
	public class Banner
	{
		public int Codigo { get; set; }
		public Local Local { get; set; }
		public string Titulo { get; set; }
		public string Caminho { get; set; }
		public DateTime DataInicial { get; set; }
		public DateTime DataFinal { get; set; }
		public bool Multimidia { get; set; }
        public bool Randomico { get; set; }
        public bool Ativo { get; set; }
        public bool AtivarCodigo { get; set; }
        public string CodigoHTML { get; set; }
	}
}