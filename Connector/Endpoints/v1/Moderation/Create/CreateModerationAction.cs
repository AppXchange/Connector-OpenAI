namespace Connector.Endpoints.v1.Moderation.Create;

using Json.Schema.Generation;
using System;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.Action;

/// <summary>
/// Action for creating a moderation check using OpenAI's API
/// </summary>
[Description("Classifies if text and/or image inputs are potentially harmful")]
public class CreateModerationAction : IStandardAction<CreateModerationActionInput, CreateModerationActionOutput>
{
    public CreateModerationActionInput ActionInput { get; set; } = new() 
    { 
        Input = string.Empty 
    };
    public CreateModerationActionOutput ActionOutput { get; set; } = new()
    {
        Id = string.Empty,
        Model = string.Empty,
        Results = Array.Empty<ModerationResult>()
    };
    public StandardActionFailure ActionFailure { get; set; } = new();

    public bool CreateRtap => true;
}

public class CreateModerationActionInput
{
    [JsonPropertyName("input")]
    [Description("Input (or inputs) to classify. Can be a single string, an array of strings, or an array of multi-modal input objects")]
    [Required]
    public required object Input { get; init; }

    [JsonPropertyName("model")]
    [Description("The content moderation model to use")]
    public string? Model { get; init; }
}

public class CreateModerationActionOutput
{
    [JsonPropertyName("id")]
    [Description("The unique identifier for the moderation request")]
    [Required]
    public required string Id { get; init; }

    [JsonPropertyName("model")]
    [Description("The model used to generate the moderation results")]
    [Required]
    public required string Model { get; init; }

    [JsonPropertyName("results")]
    [Description("A list of moderation results")]
    [Required]
    public required ModerationResult[] Results { get; init; }
}

public class ModerationResult
{
    [JsonPropertyName("flagged")]
    [Description("Whether the content was flagged as potentially harmful")]
    [Required]
    public required bool Flagged { get; init; }

    [JsonPropertyName("categories")]
    [Description("Categories of potentially harmful content")]
    [Required]
    public required ModerationCategories Categories { get; init; }

    [JsonPropertyName("category_scores")]
    [Description("Scores for each category of potentially harmful content")]
    [Required]
    public required ModerationCategoryScores CategoryScores { get; init; }

    [JsonPropertyName("category_applied_input_types")]
    [Description("Input types that were analyzed for each category")]
    public ModerationCategoryInputTypes? CategoryAppliedInputTypes { get; init; }
}

public class ModerationCategories
{
    [JsonPropertyName("sexual")]
    public bool Sexual { get; init; }

    [JsonPropertyName("hate")]
    public bool Hate { get; init; }

    [JsonPropertyName("harassment")]
    public bool Harassment { get; init; }

    [JsonPropertyName("self-harm")]
    public bool SelfHarm { get; init; }

    [JsonPropertyName("sexual/minors")]
    public bool SexualMinors { get; init; }

    [JsonPropertyName("hate/threatening")]
    public bool HateThreatening { get; init; }

    [JsonPropertyName("violence/graphic")]
    public bool ViolenceGraphic { get; init; }

    [JsonPropertyName("self-harm/intent")]
    public bool SelfHarmIntent { get; init; }

    [JsonPropertyName("self-harm/instructions")]
    public bool SelfHarmInstructions { get; init; }

    [JsonPropertyName("harassment/threatening")]
    public bool HarassmentThreatening { get; init; }

    [JsonPropertyName("violence")]
    public bool Violence { get; init; }

    [JsonPropertyName("illicit")]
    public bool Illicit { get; init; }

    [JsonPropertyName("illicit/violent")]
    public bool IllicitViolent { get; init; }
}

public class ModerationCategoryScores
{
    [JsonPropertyName("sexual")]
    public double Sexual { get; init; }

    [JsonPropertyName("hate")]
    public double Hate { get; init; }

    [JsonPropertyName("harassment")]
    public double Harassment { get; init; }

    [JsonPropertyName("self-harm")]
    public double SelfHarm { get; init; }

    [JsonPropertyName("sexual/minors")]
    public double SexualMinors { get; init; }

    [JsonPropertyName("hate/threatening")]
    public double HateThreatening { get; init; }

    [JsonPropertyName("violence/graphic")]
    public double ViolenceGraphic { get; init; }

    [JsonPropertyName("self-harm/intent")]
    public double SelfHarmIntent { get; init; }

    [JsonPropertyName("self-harm/instructions")]
    public double SelfHarmInstructions { get; init; }

    [JsonPropertyName("harassment/threatening")]
    public double HarassmentThreatening { get; init; }

    [JsonPropertyName("violence")]
    public double Violence { get; init; }

    [JsonPropertyName("illicit")]
    public double Illicit { get; init; }

    [JsonPropertyName("illicit/violent")]
    public double IllicitViolent { get; init; }
}

public class ModerationCategoryInputTypes
{
    [JsonPropertyName("sexual")]
    public string[]? Sexual { get; init; }

    [JsonPropertyName("hate")]
    public string[]? Hate { get; init; }

    [JsonPropertyName("harassment")]
    public string[]? Harassment { get; init; }

    [JsonPropertyName("self-harm")]
    public string[]? SelfHarm { get; init; }

    [JsonPropertyName("sexual/minors")]
    public string[]? SexualMinors { get; init; }

    [JsonPropertyName("hate/threatening")]
    public string[]? HateThreatening { get; init; }

    [JsonPropertyName("violence/graphic")]
    public string[]? ViolenceGraphic { get; init; }

    [JsonPropertyName("self-harm/intent")]
    public string[]? SelfHarmIntent { get; init; }

    [JsonPropertyName("self-harm/instructions")]
    public string[]? SelfHarmInstructions { get; init; }

    [JsonPropertyName("harassment/threatening")]
    public string[]? HarassmentThreatening { get; init; }

    [JsonPropertyName("violence")]
    public string[]? Violence { get; init; }

    [JsonPropertyName("illicit")]
    public string[]? Illicit { get; init; }

    [JsonPropertyName("illicit/violent")]
    public string[]? IllicitViolent { get; init; }
}
