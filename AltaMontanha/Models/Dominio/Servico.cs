using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AltaMontanha.Models.Dominio
{
    public class Servico
    {
        public virtual int Codigo { get; set; }
        public virtual string Nome { get; set; }
        public virtual string Cidade { get; set; }
        public virtual string ResumoCapa { get; set; }
        public virtual string Telefone { get; set; }
        public virtual string Logomarca { get; set; }
        public virtual string Endereco { get; set; }
        public virtual string Site { get; set; }
        public virtual string Email { get; set; }
        public virtual string Descricao { get; set; }
        public virtual string Grupo
        {
            get
            {
                return (this.Categoria == null ? 0 : this.Categoria.CodCategoria).ToString();
            }
            set
            {
                this.Categoria = new CategoriaEmpresa() { CodCategoria = int.Parse(value) };
            }
        }
        public virtual bool? Ativo { get; set; }
        public virtual CategoriaEmpresa Categoria { get; set; }
        public virtual int? Pagante { get; set; }
        public virtual bool IsPagante
        {
            get
            {
                if (!Pagante.HasValue)
                    return false;
                else
                    return Pagante.Value == 1;
            }

            set
            {
                Pagante = (value == true ? 1 : 0);
            }
        }
    }
}