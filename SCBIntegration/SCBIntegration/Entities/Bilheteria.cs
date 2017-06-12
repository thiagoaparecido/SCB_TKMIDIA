using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SCBIntegration.Entities
{

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(ElementName ="Bilheteria", Namespace = "", IsNullable = false)]
    public partial class Bilheteria
    {

        private uint registroANCINEExibidorField;

        private uint registroANCINESalaField;

        private System.DateTime diaCinematograficoField;

        private string houveSessoesField;

        private string retificadorField;
                
        private Sessao[] sessoesField;

        /// <remarks/>
        public uint registroANCINEExibidor
        {
            get
            {
                return this.registroANCINEExibidorField;
            }
            set
            {
                this.registroANCINEExibidorField = value;
            }
        }

        /// <remarks/>
        public uint registroANCINESala
        {
            get
            {
                return this.registroANCINESalaField;
            }
            set
            {
                this.registroANCINESalaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime diaCinematografico
        {
            get
            {
                return this.diaCinematograficoField;
            }
            set
            {
                this.diaCinematograficoField = value;
            }
        }

        /// <remarks/>
        public string houveSessoes
        {
            get
            {
                return this.houveSessoesField;
            }
            set
            {
                this.houveSessoesField = value;
            }
        }

        /// <remarks/>
        public string retificador
        {
            get
            {
                return this.retificadorField;
            }
            set
            {
                this.retificadorField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("sessao", IsNullable = false)]
        public Sessao[] sessoes
        {
            get
            {
                return this.sessoesField;
            }
            set
            {
                this.sessoesField = value;
            }
        }

        public string ToXml()
        {
            string xmlString = string.Empty;
            XmlSerializer serializer = new XmlSerializer(typeof(Bilheteria));
            using (StringWriter writer = new Utf8StringWriter())
            {
                serializer.Serialize(writer, this);
                writer.Flush();
                xmlString = writer.ToString();
            }
            return xmlString;
        }
    }
        

}
