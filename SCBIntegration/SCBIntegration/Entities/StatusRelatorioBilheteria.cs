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
    [System.Xml.Serialization.XmlRootAttribute(ElementName = "statusRelatorioBilheteria", Namespace = "", IsNullable = false)]
    public partial class StatusRelatorioBilheteria
    {

        private uint registroANCINEExibidorField;

        private uint registroANCINESalaField;

        private System.DateTime diaCinematograficoField;

        private string numeroProtocoloField;

        private string statusProtocoloField;

        private Mensagem[] mensagensField;

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
        public string numeroProtocolo
        {
            get
            {
                return this.numeroProtocoloField;
            }
            set
            {
                this.numeroProtocoloField = value;
            }
        }

        /// <remarks/>
        public string statusProtocolo
        {
            get
            {
                return this.statusProtocoloField;
            }
            set
            {
                this.statusProtocoloField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("mensagem", IsNullable = false)]
        public Mensagem[] mensagens
        {
            get
            {
                return this.mensagensField;
            }
            set
            {
                this.mensagensField = value;
            }
        }

        public string ToXml()
        {
            string xmlString = string.Empty;
            XmlSerializer serializer = new XmlSerializer(typeof(StatusRegistroBilheteria));
            using (StringWriter writer = new Utf8StringWriter())
            {
                serializer.Serialize(writer, this);
                writer.Flush();
                xmlString = writer.ToString();
            }
            return xmlString;
        }
    }

    /// <remarks/>
    


}
