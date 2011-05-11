﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AltaMontanha.Models.Dominio
{
	public class Foto
	{
		public virtual int Codigo { get; set; }
		public virtual string Autor { get; set; }
		public virtual string Legenda { get; set; }
		public virtual string Caminho { get; set; }
		public virtual string Fonte { get; set; }
		public virtual Foto FotoPai { get; set; }

		public Foto Clone()
		{
			return (Foto) this.MemberwiseClone();
		}
	}
}