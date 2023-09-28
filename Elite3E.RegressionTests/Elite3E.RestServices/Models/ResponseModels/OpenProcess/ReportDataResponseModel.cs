using Elite3E.RestServices.Models.ResponseModels.Common;
using Newtonsoft.Json;

namespace Elite3E.RestServices.Models.ResponseModels.OpenProcess
{
    public class ReportDataResponseModel
    {

        [JsonProperty("archetypeId")]
        public string ArchetypeId { get; set; }

        [JsonProperty("archetypeType")]
        public long ArchetypeType { get; set; }

        [JsonProperty("presentation")]
        public Presentation Presentation { get; set; }

        [JsonProperty("report")]
        public Report Report { get; set; }

        [JsonProperty("data")]
        public ReportDataResponseModelData Data { get; set; }

        [JsonProperty("filters")]
        public Filters Filters { get; set; }

        [JsonProperty("options")]
        public Options Options { get; set; }
    }

    public  class ReportDataResponseModelData
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("data")]
        public DataData Data { get; set; }
    }

    public  class DataData
    {
        [JsonProperty("attributeInformation")]
        public AttributeInformation AttributeInformation { get; set; }

        [JsonProperty("groups")]
        public List<DataGroup> Groups { get; set; }

        [JsonProperty("rows")]
        public List<object> Rows { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }

    public  class AttributeInformation
    {
        [JsonProperty("attributes")]
        public List<AttributeInformationAttribute> Attributes { get; set; }
    }

    public  class AttributeInformationAttribute
    {
        [JsonProperty("id")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Id { get; set; }

        [JsonProperty("attributeId")]
        public string AttributeId { get; set; }

        [JsonProperty("captionId")]
        public Guid CaptionId { get; set; }

        [JsonProperty("caption")]
        public string Caption { get; set; }

        [JsonProperty("dataType")]
        public long DataType { get; set; }

        [JsonProperty("isBound", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsBound { get; set; }
    }

    public  class DataGroup
    {
        [JsonProperty("dataRows")]
        public List<DataRow> DataRows { get; set; }

        [JsonProperty("groupData")]
        public GroupData GroupData { get; set; }
    }

    public class DataRow
    {
        [JsonProperty("id")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Id { get; set; }

        [JsonProperty("actions")]
        public List<DataRowAction> Actions { get; set; }

        [JsonProperty("attributes")]
        public List<DataRowAttribute> Attributes { get; set; }
    }

    public class DataRowAction
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("data")]
        public string Data { get; set; }
    }

    public class DataRowAttribute
    {
        [JsonProperty("id")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Id { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("displayValue")]
        public string DisplayValue { get; set; }

        [JsonProperty("aggregateType", NullValueHandling = NullValueHandling.Ignore)]
        public long? AggregateType { get; set; }
    }

    public class GroupData
    {
        [JsonProperty("actions")]
        public List<DataRowAction> Actions { get; set; }

        [JsonProperty("attributes")]
        public List<DataRowAttribute> Attributes { get; set; }
    }

    public class Filters
    {
        [JsonProperty("NxOpenProcesses")]
        public NxOpenProcesses NxOpenProcesses { get; set; }
    }

    public class NxOpenProcesses
    {
        [JsonProperty("archetype")]
        public string Archetype { get; set; }

        [JsonProperty("archetypeType")]
        public long ArchetypeType { get; set; }

        [JsonProperty("joins")]
        public List<object> Joins { get; set; }
    }

    public class Options
    {
        [JsonProperty("maxReportRows")]
        public long MaxReportRows { get; set; }

        [JsonProperty("defaultReportRows")]
        public long DefaultReportRows { get; set; }

        [JsonProperty("gridRowHeight")]
        public string GridRowHeight { get; set; }

        [JsonProperty("showTotals")]
        public bool ShowTotals { get; set; }
    }

    public class Presentation
    {
        [JsonProperty("downDimensions")]
        public List<object> DownDimensions { get; set; }

        [JsonProperty("pageDimensions")]
        public List<EDimension> PageDimensions { get; set; }

        [JsonProperty("reportAvailableDimensions")]
        public List<EDimension> ReportAvailableDimensions { get; set; }

        [JsonProperty("viewMode")]
        public long ViewMode { get; set; }

        [JsonProperty("boundId")]
        public string BoundId { get; set; }

        [JsonProperty("boundType")]
        public long BoundType { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("caption")]
        public string Caption { get; set; }

        [JsonProperty("type")]
        public long Type { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }
    }

    public class EDimension
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("type")]
        public long Type { get; set; }

        [JsonProperty("showSubTotalsWithSingleRows")]
        public bool ShowSubTotalsWithSingleRows { get; set; }

        [JsonProperty("showGroupWithRowsAllZeros")]
        public bool ShowGroupWithRowsAllZeros { get; set; }

        [JsonProperty("showRowsWithAllZeros")]
        public bool ShowRowsWithAllZeros { get; set; }

        [JsonProperty("indent")]
        public bool Indent { get; set; }

        [JsonProperty("showTotals")]
        public bool ShowTotals { get; set; }

        [JsonProperty("caption")]
        public string Caption { get; set; }

        [JsonProperty("sortAttributes")]
        public List<SortAttribute> SortAttributes { get; set; }
    }

    public class SortAttribute
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("sortDirection")]
        public long SortDirection { get; set; }
    }

    public class Report
    {
        [JsonProperty("paramHeader")]
        public Footer ParamHeader { get; set; }

        [JsonProperty("header")]
        public Footer Header { get; set; }

        [JsonProperty("footer")]
        public Footer Footer { get; set; }

        [JsonProperty("groups")]
        public List<ReportGroup> Groups { get; set; }

        [JsonProperty("details")]
        public List<Detail> Details { get; set; }

        [JsonProperty("boundId")]
        public string BoundId { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("caption")]
        public string Caption { get; set; }

        [JsonProperty("type")]
        public long Type { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }
    }

    public class Detail
    {
        [JsonProperty("columns")]
        public List<Column> Columns { get; set; }

        [JsonProperty("headerLevels")]
        public List<object> HeaderLevels { get; set; }

        [JsonProperty("detailType")]
        public long DetailType { get; set; }

        [JsonProperty("subclassId")]
        public string SubclassId { get; set; }

        [JsonProperty("showHeaders")]
        public bool ShowHeaders { get; set; }
    }

    public class Column
    {
        [JsonProperty("control")]
        public ColumnControl Control { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("width")]
        public long Width { get; set; }

        [JsonProperty("x", NullValueHandling = NullValueHandling.Ignore)]
        public long? X { get; set; }

        [JsonProperty("caption", NullValueHandling = NullValueHandling.Ignore)]
        public string Caption { get; set; }
    }

    public class ColumnControl
    {
        [JsonProperty("actions")]
        public List<ControlAction> Actions { get; set; }

        [JsonProperty("attributeId", NullValueHandling = NullValueHandling.Ignore)]
        public string AttributeId { get; set; }

        [JsonProperty("labelText", NullValueHandling = NullValueHandling.Ignore)]
        public string LabelText { get; set; }

        [JsonProperty("controlType")]
        public long ControlType { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("layout")]
        public Layout Layout { get; set; }
    }

    public class ControlAction
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("objectActionId")]
        public string ObjectActionId { get; set; }

        [JsonProperty("labelText")]
        public string LabelText { get; set; }
    }

    public class Layout
    {
        [JsonProperty("x")]
        public long X { get; set; }

        [JsonProperty("width")]
        public long Width { get; set; }

        [JsonProperty("height")]
        public long Height { get; set; }

        [JsonProperty("y", NullValueHandling = NullValueHandling.Ignore)]
        public long? Y { get; set; }
    }

    public class Footer
    {
        [JsonProperty("content")]
        public List<Content> Content { get; set; }

        [JsonProperty("height")]
        public long Height { get; set; }

        [JsonProperty("width")]
        public long Width { get; set; }

        [JsonProperty("paramObjID", NullValueHandling = NullValueHandling.Ignore)]
        public string ParamObjId { get; set; }
    }

    public class Content
    {
        [JsonProperty("contentType")]
        public long ContentType { get; set; }

        [JsonProperty("controls")]
        public List<ControlElement> Controls { get; set; }

        [JsonProperty("height")]
        public long Height { get; set; }
    }

    public class ControlElement
    {
        [JsonProperty("actions")]
        public List<ControlAction> Actions { get; set; }

        [JsonProperty("aggregateType", NullValueHandling = NullValueHandling.Ignore)]
        public long? AggregateType { get; set; }

        [JsonProperty("attributeId", NullValueHandling = NullValueHandling.Ignore)]
        public string AttributeId { get; set; }

        [JsonProperty("labelText", NullValueHandling = NullValueHandling.Ignore)]
        public string LabelText { get; set; }

        [JsonProperty("controlType")]
        public long ControlType { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("layout")]
        public Layout Layout { get; set; }
    }

    public class ReportGroup
    {
        [JsonProperty("header")]
        public Footer Header { get; set; }

        [JsonProperty("footer")]
        public Footer Footer { get; set; }

        [JsonProperty("groupAttributeId")]
        public string GroupAttributeId { get; set; }

        [JsonProperty("indent")]
        public bool Indent { get; set; }

        [JsonProperty("showGroupWithRowsAllZeros")]
        public bool ShowGroupWithRowsAllZeros { get; set; }

        [JsonProperty("showRowsWithAllZeros")]
        public bool ShowRowsWithAllZeros { get; set; }
    }
}
