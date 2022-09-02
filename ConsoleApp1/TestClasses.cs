using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using System.Globalization;
using System.Text.Json.Serialization;

namespace ConsoleApp1;

public class TestEntity
{
    public List<Market> Markets { get; set; }
}

[BsonIgnoreExtraElements]
[System.Diagnostics.DebuggerDisplay("{MarketTypeId} {Name} SPOV={SpecialOddsValue}")]
public class Market
{
    public long Id { get; set; }
    public string? Name { get; set; }

    [JsonIgnore]
    [BsonIgnore]
    public string? ShortName { get; set; }

    [JsonIgnore]
    public short MarketTypeId { get; set; }

    [BsonElement("SOV")]
    [Newtonsoft.Json.JsonProperty("SOV")]
    [BsonIgnoreIfNull]
    [BsonIgnoreIfDefault]
    public string? SpecialOddsValue { get; set; }

    [JsonIgnore]
    [BsonDictionaryOptions(Representation = DictionaryRepresentation.ArrayOfArrays), BsonIgnoreIfDefault]
    public Dictionary<short, string>? Specifiers { get; set; }


    [BsonIgnore]
    [JsonIgnore]
    public bool BuildUniqueIdWithSpec { get; set; } = true;

    [BsonIgnore]
    [JsonPropertyName("MarketTypeId")]
    public string MarketTypeUniqId => BuildUniqueIdWithSpec ? MarketType.GetMarketTypeUniqueId(MarketTypeId, SpecialOddsValue, HeaderIsMostBalanced && (IsMostBalanced || IsExtMostBalanced)) : MarketTypeId.ToString(CultureInfo.InvariantCulture);

    [BsonIgnore]
    public short OrgMarketTypeId => MarketTypeId;

    [BsonIgnore]
    public string? SpecialOddsTypeName { get; set; }

    [BsonIgnore]
    public string? SpecialOddsName { get; set; }

    [JsonPropertyName("Items")]
    [BsonElement("CI")]
    [Newtonsoft.Json.JsonProperty("CI")]
    public List<Selection> ChildItems { get; set; } = default!;

#pragma warning restore CA1822 // Mark members as static

    [BsonIgnore]
    public int ColumnCount { get; set; }

    [BsonIgnore]
    public short MobileColumnCount { get; set; }

    [JsonIgnore]
    [BsonIgnoreIfDefault]
    public bool GroupMarketsByType { get; set; }

    [JsonIgnore]
    [BsonIgnore]
    public decimal SpovAsDecimal { get; set; }

    [JsonIgnore]
    [BsonIgnore]
    public decimal? PriceOfFirstColumn { get; set; }

    /// <summary>
    /// For QA will be removed after test
    /// </summary>
    public DateTime InvalidateDate { get; set; }

    public byte Status { get; set; }

    [JsonIgnore]
    public long EventId { get; set; }

    public byte ProviderId { get; set; }

    [BsonElement("SMId")]
    [Newtonsoft.Json.JsonProperty("SMId")]    
    public int SportMarketId { get; set; }

    [BsonIgnoreIfNull]
    [BsonElement("DO")]
    [Newtonsoft.Json.JsonProperty("DO")]
    [JsonIgnore]
    public int? DisplayOrder { get; set; }

    [BsonElement("IsMB")]
    [Newtonsoft.Json.JsonProperty("IsMB")]
    [BsonIgnoreIfDefault]
    public bool IsMostBalanced { get; set; }

    [BsonElement("IsExtMB")]
    [Newtonsoft.Json.JsonProperty("IsExtMB")]
    [BsonIgnoreIfDefault]
    public bool IsExtMostBalanced { get; set; }

    [JsonIgnore]
    [BsonIgnore]
    public bool HeaderIsMostBalanced { get; set; }

    [BsonIgnore]
    public bool HeaderIsMostBalancedWidgets { get; set; }

    [BsonIgnore]
    public bool HeaderIsExtMostBalancedWidgets { get; set; }

    [BsonIgnoreIfDefault]
    public bool IsZeroMargin { get; set; }

    [BsonIgnore]
    public byte DisplayTemplate { get; set; }

    

    [BsonElement("IA")]
    [Newtonsoft.Json.JsonProperty("IA")]
    [BsonIgnoreIfDefault]
    public bool IsAsian { get; set; }

    [Obsolete("Legacy property, which is required due to shortcomings on the STGE side")]
    [BsonIgnore]
    public bool IsBetBuilder { get; set; }

    [BsonElement("AvBB")]
    [Newtonsoft.Json.JsonProperty("AvBB")]
    [BsonIgnoreIfDefault]
    public bool IsAvailableForBB { get; set; }

    [BsonIgnore]
    public bool HasMoreSelections { get; set; }

    [BsonIgnore]
    [JsonIgnore]
    public bool HideInActiveSelections { get; set; }

    [BsonIgnoreIfNull]
    public List<Market>? ChildMarkets { get; set; }

    [BsonIgnoreIfNull]
    [JsonIgnore]
    public List<MarketHeader>? Headers { get; set; }

    
    [BsonIgnore]
    [JsonIgnore]
    public int? SpovSecondPart => Specifiers?.Count > 1
            && int.TryParse(Specifiers.ElementAt(0).Value, out var m)
            && int.TryParse(Specifiers.ElementAt(1).Value, out var k) ? m * 1_000 + k : default(int?);

    [BsonIgnoreIfNull]
    public byte? PeriodSpecifierId { get; set; }

    [BsonIgnoreIfNull]
    public byte? PeriodSpecifierValue { get; set; }

    [BsonElement("AOIds"), BsonIgnoreIfNull]
    public int[]? AamsOperatorIds { get; set; }

    [BsonElement("L")]
    [Newtonsoft.Json.JsonProperty("L")]
    public bool IsLive { get; set; }

    [BsonIgnoreIfNull]
    [BsonElement("MinP")]
    [Newtonsoft.Json.JsonProperty("MinP")]
    public decimal? MinPrice { get; set; }

    [BsonIgnore]
    public bool ReverseCompetitors { get; set; }

    [BsonIgnore]
    public bool IgnoreMarketOut { get; set; }

    [BsonIgnore]
    public bool IsEarlyPayout { get; set; }

    [BsonIgnore]
    public bool IsBoreDraw { get; set; }

    [BsonIgnore]
    public bool IsEmpty => ChildItems?.Count == 0 && ChildMarkets?.Count == 0;

    [BsonIgnore]
    public int? IntCompetitorId { get; set; }


}

[BsonIgnoreExtraElements]
[System.Diagnostics.DebuggerDisplay("{SelectionTypeId} {Name}")]
public class Selection 
{
    public long Id { get; set; }

    [BsonElement("N")]
    [Newtonsoft.Json.JsonProperty("N")]
    public string? Name { get; set; }

    [BsonElement("R")]
    [Newtonsoft.Json.JsonProperty("R")]
    public short Result { get; set; }

    [BsonRepresentation(BsonType.Double)]
    [BsonElement("Pr")]
    [Newtonsoft.Json.JsonProperty("Pr")]
    public decimal? Price { get; set; }

    [JsonIgnore]
    [BsonElement("Prs")]
    [Newtonsoft.Json.JsonProperty("Prs")]
    [BsonIgnoreIfDefault]
    [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfDocuments)]
    public Dictionary<short, decimal?>? Prices { get; set; }

    

    [BsonElement("STId")]
    [Newtonsoft.Json.JsonProperty("STId")]
    public short SelectionTypeId { get; set; }

    [BsonElement("MSId")]
    [Newtonsoft.Json.JsonProperty("MSId")]
    public int MarketSelectionId { get; set; }

    [BsonIgnore]
    [JsonPropertyName("SMId"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public int SportMarketId { get; set; }

    [BsonIgnore]

    public string? FastCode { get; set; }

    [BsonIgnore]
#pragma warning disable CA1721 // Property names should not match get methods: I can't rename it to anything
    public string? SPOV { get; set; }
#pragma warning restore CA1721 // Property names should not match get methods

    [BsonIgnoreIfNull]
    [JsonIgnore]
    public decimal? Probability { get; set; }

    [BsonElement("RP")]
    [Newtonsoft.Json.JsonProperty("RP")]
    [BsonIgnoreIfNull]
    [JsonIgnore]
    public decimal? RefundProbability { get; set; }

    [BsonElement("HLP")]
    [Newtonsoft.Json.JsonProperty("HLP")]
    [BsonIgnoreIfNull]
    [JsonIgnore]
    public decimal? HalfLoseProbability { get; set; }

    [BsonElement("HWP")]
    [Newtonsoft.Json.JsonProperty("HWP")]
    [BsonIgnoreIfNull]
    [JsonIgnore]
    public decimal? HalfWinProbability { get; set; }

    [BsonElement("WP")]
    [Newtonsoft.Json.JsonProperty("WP")]
    [BsonIgnoreIfNull]
    [JsonIgnore]
    public decimal? WinProbability { get; set; }

    public bool IsActive { get; set; }

    [BsonIgnore]
    [JsonIgnore]
    public short SortOrder { get; set; }

    [BsonIgnore]
    public short[] ColumnNums => new[] { ColumnNum, MobileColumnNum };

    [BsonIgnore]
    public short MobileColumnNum { get; set; }

    [BsonIgnore]
    public byte ColumnNum { get; set; }

    [BsonIgnoreIfNull]
    [JsonIgnore]
    public string? ExtId { get; set; }

    [Obsolete("Legacy property, which is required due to shortcomings on the STGE side")]
    [BsonIgnoreIfNull]
    [BsonElement("BBST")]
    [Newtonsoft.Json.JsonProperty("BBST")]

    public short? BBSelectionType { get; set; }

    /// <summary>
    /// Selection icon used in
    /// markets like 1 Minute.
    /// </summary>
    /// 
    /// <remarks>
    /// At the moment, filling occurs through the mapping between this 
    /// property and  <seealso cref="SelectionsOrder.SelectionIcon"/>.
    /// This is due to the fact that when invalidated, the value saved 
    /// at the selection level is overwritten!
    /// </remarks>
    [BsonIgnore]

    public string? SelectionIcon { get; set; }

    [BsonIgnore]
    public byte MB { get; set; }

    

    [BsonIgnore]
    [JsonIgnore]
    public bool IsAsian { get; set; }

    [BsonIgnoreIfNull]
    [BsonElement("CId")]
    [Newtonsoft.Json.JsonProperty("CId")]
    [JsonIgnore]
    public int? CompetitorId { get; set; }

    [BsonElement("IsMB")]
    [Newtonsoft.Json.JsonProperty("IsMB")]
    [BsonIgnoreIfDefault]
    [JsonIgnore]
    public bool IsMostBalanced { get; set; }

    [BsonElement("IsExtMB")]
    [Newtonsoft.Json.JsonProperty("IsExtMB")]
    [BsonIgnoreIfDefault]
    [JsonIgnore]
    public bool IsExtMostBalanced { get; set; }

    [BsonIgnoreIfNull]
    [BsonElement("PId")]
    [Newtonsoft.Json.JsonProperty("PId")]
    [JsonIgnore]
    public int? PlayerId { get; set; }

    [BsonIgnore]
    [JsonIgnore]
    public int? IntPlayerId { get; set; } //USED FOR AAMS IN MIS

    //TODO: Maybe it makes sense to store the sov in db, it will be easier to apply sorts in API and easy to store in db
    [BsonIgnore]
    [JsonIgnore]
    public decimal SpovAsDecimal { get; set; }

    /// <summary>
    /// Actual for Outright selection translations
    /// </summary>
    [JsonIgnore]
    [BsonIgnoreIfNull]
    [BsonElement("STr")]
    [Newtonsoft.Json.JsonProperty("STr")]
    public TranslationsWrapper? SelectionTranslations { get; set; }

    [JsonIgnore]
    [BsonIgnore]
    public EventPlayer? Player { get; set; }

    [BsonElement("AOIds"), BsonIgnoreIfNull]
    [Newtonsoft.Json.JsonProperty("AOIds")]
    [JsonIgnore]
    public int[]? AamsOperatorIds { get; set; }

    [BsonElement("DbyC"), BsonIgnoreIfDefault]
    [Newtonsoft.Json.JsonProperty("DbyC")]
    [JsonIgnore]
    public bool IsDisabledByCompetitors { get; set; }

    [BsonIgnore]
    [JsonIgnore]
    public decimal SpecialOddsValueAsDecimal { get; set; }

    public decimal GetSPOV() => SpovAsDecimal; // See: SB2-3624.

    public Selection Clone() => (Selection)MemberwiseClone();

    [BsonIgnore]
    [JsonPropertyName("EP"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool IsEarlyPayout { get; set; }

    [BsonIgnore]
    [JsonPropertyName("BD"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool IsBoreDraw { get; set; }
}
[BsonIgnoreExtraElements]
public class EventPlayer
{
    public int PlayerId { get; set; }
    [BsonIgnoreIfNull]
    public int? IntPlayerId { get; set; }
    public string? Name { get; set; }
    public string? ExtId { get; set; }
    [BsonElement("CId"), BsonIgnoreIfNull]
    public int? IntCompetitorId { get; set; }
}
[BsonIgnoreExtraElements]
public class MarketType
{
    private const string MOST_BALANCED = "mb";

    [JsonPropertyName("MarketTypeId")]
    [BsonIgnore]
    public virtual string Id => GetMarketTypeUniqueId(MarketTypeId, SpecOdd?.Value);

    [JsonIgnore]
    public short MarketTypeId { get; set; }
    public string? Name { get; set; }
    [BsonIgnoreIfDefault]
    public SpecOdd? SpecOdd { get; set; }

    public static string GetMarketTypeUniqueId(short marketTypeId, string? specialOddsValue, bool isMostBalanced = false)
    {
        var spov = isMostBalanced ? MOST_BALANCED : specialOddsValue;
        if (!string.IsNullOrEmpty(spov))
            spov = spov!.Trim();
        return $"{marketTypeId}_{spov}".Trim();
    }
}

[BsonIgnoreExtraElements]
public class SelectionType
{
    [JsonPropertyName("Id")]
    public short SelectionTypeId { get; set; }
    public string? Name { get; set; }
    [BsonElement("SO")]
    public short SortOrder { get; set; }
    public int MarketSelectionId { get; set; }
}


[BsonIgnoreExtraElements]
public class SpecOdd
{
    public byte? TypeId { get; set; }

    public string? Value { get; set; }

    [JsonIgnore]
    [BsonDictionaryOptions(Representation = DictionaryRepresentation.ArrayOfArrays)]
    public Dictionary<short, string>? Specifiers { get; set; }

    [JsonIgnore]
    [BsonElement("SpecTr")]
    public TranslationsWrapper? SpecialOddsTypeTranslations { get; set; }

    [JsonIgnore]
    [BsonElement("SpecMrTr"), BsonIgnoreIfNull]
    public TranslationsWrapper? SpecialOddsTypeMarketTranslations { get; set; }

    [BsonIgnore]
    public string? Name { get; set; }
}
[BsonIgnoreExtraElements]
[System.Diagnostics.DebuggerDisplay("{MarketTypeId} {Name} IsMostB:{UseMostBalanced || UseExtMostBalanced } SPOV:{SpecOdd?.Value}")]
public class MarketHeader : MarketType
{
    private const string ALL_MOST_BALANCED = "all";

    [JsonPropertyName("MarketTypeId")]
    public string? HeaderMarketTypeId { get; set; }

    public string GenerateHeaderId(bool useAll, bool isMarketPeriod)
    {
        if (useAll)
            return $"{MarketTypeId}_{ALL_MOST_BALANCED}";
        return GetMarketTypeUniqueId(MarketTypeId, SpecOdd?.Value, !isMarketPeriod && UseMostBalanced);
    }

    [JsonPropertyName("Items")]
    public List<SelectionType>? ChildItems { get; set; }


    public int SortOrder { get; set; }
    [BsonIgnoreIfDefault]
    public bool UseMostBalanced { get; set; }
    [BsonIgnoreIfDefault]
    public bool UseExtMostBalanced { get; set; }
    public byte DisplayTemplate { get; set; }
    [BsonIgnoreIfDefault]
    public int ColumnCount { get; set; }
    [BsonIgnoreIfDefault]
    public int MobileColumnCount { get; set; }

    [JsonIgnore]
    public bool IgnoreSelectionTranslation
    {
        get;
        set;
    }
}
public class TranslationsWrapper
{
    public string? Name { get; set; }
    public List<TranslationItem>? Translations { get; set; }

    //public string TranslationsXml
    //{
    //    set
    //    {
    //        if (string.IsNullOrEmpty(value))
    //        {
    //            Translations = null;
    //            return;
    //        }
    //        var xml = XDocument.Parse(value);
    //        Translations = xml.Descendants("root")
    //            .Elements()
    //            .Select(tr => new TranslationItem
    //            {
    //                LangId = Convert.ToInt16(tr.Attribute("l")?.Value, CultureInfo.InvariantCulture),
    //                Value = Convert.ToString(tr.Attribute("v")?.Value, CultureInfo.InvariantCulture)
    //            }).ToList();
    //    }
    //}

    public string? ToString(short? langId) =>
        Translations?.FirstOrDefault(tr => tr.LangId == langId)?.Value ?? Name;
}
[BsonIgnoreExtraElements]
public class TranslationItem
{
    //[XmlElement("l")]
    [BsonElement("l")]
    public short LangId { get; set; }
    //[XmlElement("v")]
    [BsonElement("v")]
    public string? Value { get; set; }
}



