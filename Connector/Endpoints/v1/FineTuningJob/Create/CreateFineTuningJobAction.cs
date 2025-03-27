namespace Connector.Endpoints.v1.FineTuningJob.Create;

using Json.Schema.Generation;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.Action;

/// <summary>
/// Action for creating a fine-tuning job using OpenAI's API
/// </summary>
[Description("Creates a fine-tuning job which begins the process of creating a new model from a given dataset")]
public class CreateFineTuningJobAction : IStandardAction<CreateFineTuningJobActionInput, CreateFineTuningJobActionOutput>
{
    public CreateFineTuningJobActionInput ActionInput { get; set; } = new() 
    { 
        Model = string.Empty,
        TrainingFile = string.Empty
    };
    public CreateFineTuningJobActionOutput ActionOutput { get; set; } = new()
    {
        Id = string.Empty,
        Object = "fine_tuning.job",
        Model = string.Empty,
        CreatedAt = 0,
        OrganizationId = string.Empty,
        ResultFiles = Array.Empty<string>(),
        Status = string.Empty,
        TrainingFile = string.Empty,
        Method = new FineTuningMethod { Type = string.Empty }
    };
    public StandardActionFailure ActionFailure { get; set; } = new();

    public bool CreateRtap => true;
}

public class CreateFineTuningJobActionInput
{
    [JsonPropertyName("model")]
    [Description("The name of the model to fine-tune")]
    [Required]
    public required string Model { get; init; }

    [JsonPropertyName("training_file")]
    [Description("The ID of an uploaded file that contains training data")]
    [Required]
    public required string TrainingFile { get; init; }

    [JsonPropertyName("validation_file")]
    [Description("The ID of an uploaded file that contains validation data")]
    public string? ValidationFile { get; init; }

    [JsonPropertyName("hyperparameters")]
    [Description("The hyperparameters used for the fine-tuning job")]
    public Hyperparameters? Hyperparameters { get; init; }

    [JsonPropertyName("integrations")]
    [Description("A list of integrations to enable for your fine-tuning job")]
    public object[]? Integrations { get; init; }

    [JsonPropertyName("metadata")]
    [Description("Set of key-value pairs that can be attached to the job")]
    public Dictionary<string, string>? Metadata { get; init; }

    [JsonPropertyName("method")]
    [Description("The method used for fine-tuning")]
    public FineTuningMethod? Method { get; init; }

    [JsonPropertyName("seed")]
    [Description("The seed controls the reproducibility of the job")]
    public int? Seed { get; init; }

    [JsonPropertyName("suffix")]
    [Description("A string that will be added to your fine-tuned model name")]
    public string? Suffix { get; init; }
}

public class CreateFineTuningJobActionOutput
{
    [JsonPropertyName("id")]
    [Description("The unique identifier for the fine-tuning job")]
    [Required]
    public required string Id { get; init; }

    [JsonPropertyName("object")]
    [Description("The object type, which is always 'fine_tuning.job'")]
    [Required]
    public required string Object { get; init; }

    [JsonPropertyName("model")]
    [Description("The base model that is being fine-tuned")]
    [Required]
    public required string Model { get; init; }

    [JsonPropertyName("created_at")]
    [Description("The Unix timestamp (in seconds) for when the job was created")]
    [Required]
    public required long CreatedAt { get; init; }

    [JsonPropertyName("fine_tuned_model")]
    [Description("The name of the fine-tuned model that is being created")]
    public string? FineTunedModel { get; init; }

    [JsonPropertyName("organization_id")]
    [Description("The organization that owns the fine-tuning job")]
    [Required]
    public required string OrganizationId { get; init; }

    [JsonPropertyName("result_files")]
    [Description("The compiled results files for the fine-tuning job")]
    [Required]
    public required string[] ResultFiles { get; init; }

    [JsonPropertyName("status")]
    [Description("The current status of the fine-tuning job")]
    [Required]
    public required string Status { get; init; }

    [JsonPropertyName("validation_file")]
    [Description("The validation file used for the fine-tuning job")]
    public string? ValidationFile { get; init; }

    [JsonPropertyName("training_file")]
    [Description("The training file used for the fine-tuning job")]
    [Required]
    public required string TrainingFile { get; init; }

    [JsonPropertyName("method")]
    [Description("The method used for fine-tuning")]
    [Required]
    public required FineTuningMethod Method { get; init; }

    [JsonPropertyName("metadata")]
    [Description("Optional metadata associated with the fine-tuning job")]
    public Dictionary<string, string>? Metadata { get; init; }
}

public class Hyperparameters
{
    [JsonPropertyName("n_epochs")]
    [Description("The number of epochs for fine-tuning")]
    public string? NEpochs { get; init; }

    [JsonPropertyName("batch_size")]
    [Description("The batch size for fine-tuning")]
    public string? BatchSize { get; init; }

    [JsonPropertyName("learning_rate_multiplier")]
    [Description("The learning rate multiplier for fine-tuning")]
    public string? LearningRateMultiplier { get; init; }
}

public class FineTuningMethod
{
    [JsonPropertyName("type")]
    [Description("The type of fine-tuning method")]
    [Required]
    public required string Type { get; init; }

    [JsonPropertyName("supervised")]
    [Description("The supervised fine-tuning parameters")]
    public SupervisedParameters? Supervised { get; init; }
}

public class SupervisedParameters
{
    [JsonPropertyName("hyperparameters")]
    [Description("The hyperparameters for supervised fine-tuning")]
    [Required]
    public required Hyperparameters Hyperparameters { get; init; }
}
