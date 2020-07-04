namespace Demo.Exchange.Domain.AggregateModel.TaxaModel
{
    using Demo.Exchange.Domain.SeedWorks;
    using System;
    using System.Linq;

    public class TipoSegmento : Enumeration
    {
        public TipoSegmento(string id, string name, string desricaoSimples)
            : base(id, name)
        {
            DesricaoSimples = desricaoSimples;
        }

        public static readonly TipoSegmento Varejo = new TipoSegmento("VAREJO", nameof(Varejo), "Segmento para cliente do varejo");
        public static readonly TipoSegmento Personnalite = new TipoSegmento("PERSONNALITE", nameof(Personnalite), "Segmento para cliente do varejo");
        public static readonly TipoSegmento Private = new TipoSegmento("PRIVATE", nameof(Private), "Segmento para cliente do varejo");

        public string DesricaoSimples { get; }

        public static TipoSegmento ObterPorId(string id)
            => GetAll<TipoSegmento>().SingleOrDefault(x => x.Id.Equals(id, StringComparison.InvariantCultureIgnoreCase));
    }
}