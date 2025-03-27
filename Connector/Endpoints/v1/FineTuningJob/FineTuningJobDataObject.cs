namespace Connector.Endpoints.v1.FineTuningJob;

using Json.Schema.Generation;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.CacheWriter;

/// <summary>
/// Represents a fine-tuning job from OpenAI's API
/// </summary>
[PrimaryKey("id", nameof(Id))]
[Description("Represents a fine-tuning job from OpenAI's API")]
public class FineTuningJobDataObject
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

    [JsonPropertyName("finished_at")]
    [Description("The Unix timestamp (in seconds) for when the job was finished")]
    public long? FinishedAt { get; init; }

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

    [JsonPropertyName("hyperparameters")]
    [Description("The hyperparameters used for fine-tuning")]
    [Required]
    public required Hyperparameters Hyperparameters { get; init; }

    [JsonPropertyName("trained_tokens")]
    [Description("The number of tokens trained")]
    [Required]
    public required int TrainedTokens { get; init; }

    [JsonPropertyName("integrations")]
    [Description("The integrations associated with the fine-tuning job")]
    [Required]
    public required object[] Integrations { get; init; }

    [JsonPropertyName("seed")]
    [Description("The seed used for fine-tuning")]
    [Required]
    public required int Seed { get; init; }

    [JsonPropertyName("estimated_finish")]
    [Description("The estimated finish time in Unix timestamp")]
    public long? EstimatedFinish { get; init; }

    [JsonPropertyName("method")]
    [Description("The method used for fine-tuning")]
    [Required]
    public required FineTuningMethod Method { get; init; }
}

public class Hyperparameters
{
    [JsonPropertyName("n_epochs")]
    [Description("The number of epochs for fine-tuning")]
    [Required]
    public required int NEpochs { get; init; }

    [JsonPropertyName("batch_size")]
    [Description("The batch size for fine-tuning")]
    [Required]
    public required int BatchSize { get; init; }

    [JsonPropertyName("learning_rate_multiplier")]
    [Description("The learning rate multiplier for fine-tuning")]
    [Required]
    public required double LearningRateMultiplier { get; init; }
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