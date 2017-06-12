using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCBIntegration.Entities
{
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class TotalizacaoCategoriaIngresso
    {

        private uint codigoCategoriaIngressoField;

        private uint quantidadeEspectadoresField;

        private TotalizacaoModalidadePagamento[] totalizacoesModalidadePagamentoField;

        /// <remarks/>
        public uint codigoCategoriaIngresso
        {
            get
            {
                return this.codigoCategoriaIngressoField;
            }
            set
            {
                this.codigoCategoriaIngressoField = value;
            }
        }

        /// <remarks/>
        public uint quantidadeEspectadores
        {
            get
            {
                return this.quantidadeEspectadoresField;
            }
            set
            {
                this.quantidadeEspectadoresField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("totalizacaoModalidadePagamento", IsNullable = false)]
        public TotalizacaoModalidadePagamento[] totalizacoesModalidadePagamento
        {
            get
            { 
                return this.totalizacoesModalidadePagamentoField;
            }
            set
            {
                this.totalizacoesModalidadePagamentoField = value;
            }
        }
    }
}
