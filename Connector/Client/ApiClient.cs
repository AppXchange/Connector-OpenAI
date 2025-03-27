using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using Connector.Endpoints.v1.Transcription;
using Connector.Endpoints.v1.Transcription.Create;
using Connector.Endpoints.v1.Translation;
using Connector.Endpoints.v1.Translation.Create;
using Connector.Endpoints.v1.Speech;
using Connector.Endpoints.v1.Speech.Create;
using Connector.Endpoints.v1.ChatCompletion;
using Connector.Endpoints.v1.ChatCompletion.Create;
using Connector.Endpoints.v1.ChatCompletion.Delete;
using System.Text.Json;
using Connector.Endpoints.v1.ChatCompletion.Update;
using Connector.Endpoints.v1.ChatCompletionList;
using Connector.Endpoints.v1.ChatMessages;
using System;
using Connector.Endpoints.v1.Embeddings;
using Connector.Endpoints.v1.Embeddings.Create;
using Connector.Endpoints.v1.Moderation;
using Connector.Endpoints.v1.Moderation.Create;
using System.Text.Json.Serialization;
using System.Text;
using Connector.Endpoints.v1.Batch;
using Connector.Endpoints.v1.Batch.Create;
using Connector.Endpoints.v1.Batch.Cancel;
using Connector.Endpoints.v1.BatchList;
using Connector.Endpoints.v1.File;
using System.IO;
using Connector.Endpoints.v1.File.Upload;
using Connector.Endpoints.v1.File.Delete;
using Connector.Endpoints.v1.FileList;
using Connector.Endpoints.v1.FineTuningCheckpoints;
using Connector.Endpoints.v1.FineTuningEvents;
using Connector.Endpoints.v1.FineTuningJobs;
using Connector.Endpoints.v1.FineTuningJob;
using Connector.Endpoints.v1.FineTuningJob.Create;
using Connector.Endpoints.v1.FineTuningJob.Cancel;
using Connector.Endpoints.v1.Image.Create;
using Connector.Endpoints.v1.Image.CreateEdit;
using Connector.Endpoints.v1.Image.CreateVariation;
using Connector.Endpoints.v1.Model;
using Connector.Endpoints.v1.Model.Delete;
using Connector.Endpoints.v1.ModelList;
using Connector.Endpoints.v1.Upload;
using Connector.Endpoints.v1.Upload.Create;
using Connector.Endpoints.v1.Upload.AddPart;
using Connector.Endpoints.v1.Upload.Cancel;
using Connector.Endpoints.v1.Upload.Complete;
using Connector.Assistants.v1.Assistant;
using Connector.Assistants.v1.Assistant.Create;
using Connector.Assistants.v1.Assistant.Delete;
using Connector.Assistants.v1.Assistant.Modify;
using Connector.Assistants.v1.ListAssistants;
using System.Linq;
using Connector.Assistants.v1.Thread;
using Connector.Assistants.v1.Thread.Create;
using Connector.Assistants.v1.Thread.Delete;
using Connector.Assistants.v1.Thread.Modify;
using Connector.Assistants.v1.Message;
using Connector.Assistants.v1.Message.Create;
using Connector.Assistants.v1.Message.Delete;
using Connector.Assistants.v1.Message.Modify;
using Connector.Assistants.v1.ListMessages;
using Connector.Assistants.v1.Run;
using Connector.Assistants.v1.Run.Cancel;
using Connector.Assistants.v1.Run.Create;
using Connector.Assistants.v1.Run.CreateThread;
using Connector.Assistants.v1.Run.Modify;
using Connector.Assistants.v1.Run.SubmitToolOutputs;
using Connector.Assistants.v1.ListRuns;
using Connector.Assistants.v1.RunStep;
using Connector.Assistants.v1.ListRunSteps;
using Connector.Assistants.v1.VectorStore;
using Connector.Assistants.v1.VectorStore.Create;
using Connector.Assistants.v1.VectorStore.Delete;
using Connector.Assistants.v1.VectorStore.Modify;
using Connector.Assistants.v1.ListVectorStores;
using Connector.Assistants.v1.VectorStoreFile;
using Connector.Assistants.v1.VectorStoreFile.Create;
using Connector.Assistants.v1.VectorStoreFile.Delete;
using Connector.Assistants.v1.VectorStoreFile.FileContent;
using Connector.Assistants.v1.ListVectorStoreFiles;
using Connector.Assistants.v1.VectorStoreFileInBatch;
using Connector.Assistants.v1.VectorStoreFileInBatch.Cancel;
using Connector.Assistants.v1.VectorStoreFileInBatch.Create;
using Connector.Assistants.v1.ListVectorStoreFilesInBatch;

namespace Connector.Client;

/// <summary>
/// A client for interfacing with the API via the HTTP protocol.
/// </summary>
public class ApiClient
{
    private readonly HttpClient _httpClient;

    public ApiClient (HttpClient httpClient, string baseUrl)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new System.Uri(baseUrl);
    }

    // Example of a paginated response.
    public async Task<ApiResponse<PaginatedResponse<T>>> GetRecords<T>(string relativeUrl, int page, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync($"{relativeUrl}?page={page}", cancellationToken: cancellationToken).ConfigureAwait(false);
        return new ApiResponse<PaginatedResponse<T>>
        {
            IsSuccessful = response.IsSuccessStatusCode,
            StatusCode = (int)response.StatusCode,
            Data = response.IsSuccessStatusCode ? await response.Content.ReadFromJsonAsync<PaginatedResponse<T>>(cancellationToken: cancellationToken) : default,
            RawResult = await response.Content.ReadAsStreamAsync(cancellationToken: cancellationToken)
        };
    }

    public async Task<ApiResponse> GetNoContent(CancellationToken cancellationToken = default)
    {
        var response = await _httpClient
            .GetAsync("no-content", cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        return new ApiResponse
        {
            IsSuccessful = response.IsSuccessStatusCode,
            StatusCode = (int)response.StatusCode,
            RawResult = await response.Content.ReadAsStreamAsync(cancellationToken: cancellationToken)
        };
    }

    public async Task<ApiResponse> TestConnection(CancellationToken cancellationToken = default)
    {
        // The purpose of this method is to validate that successful and authorized requests can be made to the API.
        // In this example, we are using the GET "oauth/me" endpoint.
        // Choose any endpoint that you consider suitable for testing the connection with the API.

        var response = await _httpClient
            .GetAsync($"oauth/me", cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        return new ApiResponse
        {
            IsSuccessful = response.IsSuccessStatusCode,
            StatusCode = (int)response.StatusCode,
        };
    }

    public async Task<ApiResponse<CreateTranscriptionActionOutput>> CreateTranscription(CreateTranscriptionActionInput input, CancellationToken cancellationToken = default)
    {
        using var formData = new MultipartFormDataContent();
        formData.Add(new ByteArrayContent(input.File), "file", "audio.mp3");
        formData.Add(new StringContent(input.Model), "model");
        
        if (!string.IsNullOrEmpty(input.Language))
            formData.Add(new StringContent(input.Language), "language");
        
        if (!string.IsNullOrEmpty(input.Prompt))
            formData.Add(new StringContent(input.Prompt), "prompt");
        
        if (!string.IsNullOrEmpty(input.ResponseFormat))
            formData.Add(new StringContent(input.ResponseFormat), "response_format");
        
        if (input.Temperature.HasValue)
            formData.Add(new StringContent(input.Temperature.Value.ToString()), "temperature");
        
        if (input.TimestampGranularities != null && input.TimestampGranularities.Length > 0)
            formData.Add(new StringContent(string.Join(",", input.TimestampGranularities)), "timestamp_granularities");

        var response = await _httpClient
            .PostAsync("audio/transcriptions", formData, cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        return new ApiResponse<CreateTranscriptionActionOutput>
        {
            IsSuccessful = response.IsSuccessStatusCode,
            StatusCode = (int)response.StatusCode,
            Data = response.IsSuccessStatusCode ? await response.Content.ReadFromJsonAsync<CreateTranscriptionActionOutput>(cancellationToken: cancellationToken) : default,
            RawResult = await response.Content.ReadAsStreamAsync(cancellationToken: cancellationToken)
        };
    }

    public async Task<ApiResponse<PaginatedResponse<TranscriptionDataObject>>> GetTranscriptions(int page, CancellationToken cancellationToken = default)
    {
        return await GetRecords<TranscriptionDataObject>("transcriptions", page, cancellationToken);
    }

    public async Task<ApiResponse<CreateTranslationActionOutput>> CreateTranslation(CreateTranslationActionInput input, CancellationToken cancellationToken = default)
    {
        using var formData = new MultipartFormDataContent();
        formData.Add(new ByteArrayContent(input.File), "file", "audio.mp3");
        formData.Add(new StringContent(input.Model), "model");
        
        if (!string.IsNullOrEmpty(input.Prompt))
            formData.Add(new StringContent(input.Prompt), "prompt");
        
        if (!string.IsNullOrEmpty(input.ResponseFormat))
            formData.Add(new StringContent(input.ResponseFormat), "response_format");
        
        if (input.Temperature.HasValue)
            formData.Add(new StringContent(input.Temperature.Value.ToString()), "temperature");

        var response = await _httpClient
            .PostAsync("audio/translations", formData, cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        return new ApiResponse<CreateTranslationActionOutput>
        {
            IsSuccessful = response.IsSuccessStatusCode,
            StatusCode = (int)response.StatusCode,
            Data = response.IsSuccessStatusCode ? await response.Content.ReadFromJsonAsync<CreateTranslationActionOutput>(cancellationToken: cancellationToken) : default,
            RawResult = await response.Content.ReadAsStreamAsync(cancellationToken: cancellationToken)
        };
    }

    public async Task<ApiResponse<PaginatedResponse<TranslationDataObject>>> GetTranslations(int page, CancellationToken cancellationToken = default)
    {
        return await GetRecords<TranslationDataObject>("translations", page, cancellationToken);
    }

    private async Task<ApiResponse<T>> PostAsync<T>(string relativeUrl, HttpContent content, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient
            .PostAsync(relativeUrl, content, cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        return new ApiResponse<T>
        {
            IsSuccessful = response.IsSuccessStatusCode,
            StatusCode = (int)response.StatusCode,
            Data = response.IsSuccessStatusCode ? await response.Content.ReadFromJsonAsync<T>(cancellationToken: cancellationToken) : default,
            RawResult = await response.Content.ReadAsStreamAsync(cancellationToken: cancellationToken)
        };
    }

    public async Task<ApiResponse<CreateSpeechActionOutput>> CreateSpeech(CreateSpeechActionInput input, CancellationToken cancellationToken = default)
    {
        var formData = new MultipartFormDataContent();
        formData.Add(new StringContent(input.Input), "input");
        formData.Add(new StringContent(input.Model), "model");
        formData.Add(new StringContent(input.Voice), "voice");
        
        if (!string.IsNullOrEmpty(input.Instructions))
        {
            formData.Add(new StringContent(input.Instructions), "instructions");
        }
        
        if (!string.IsNullOrEmpty(input.ResponseFormat))
        {
            formData.Add(new StringContent(input.ResponseFormat), "response_format");
        }
        
        if (input.Speed.HasValue)
        {
            formData.Add(new StringContent(input.Speed.Value.ToString()), "speed");
        }

        return await PostAsync<CreateSpeechActionOutput>("audio/speech", formData, cancellationToken);
    }

    public async Task<ApiResponse<PaginatedResponse<SpeechDataObject>>> GetSpeeches(int page, CancellationToken cancellationToken = default)
    {
        return await GetRecords<SpeechDataObject>("speeches", page, cancellationToken);
    }

    public async Task<ApiResponse<PaginatedResponse<ChatCompletionDataObject>>> GetChatCompletions(int page, CancellationToken cancellationToken = default)
    {
        return await GetRecords<ChatCompletionDataObject>("chat/completions", page, cancellationToken);
    }

    public async Task<ApiResponse<ChatCompletionDataObject>> GetChatCompletion(string completionId, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient
            .GetAsync($"chat/completions/{completionId}", cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        return new ApiResponse<ChatCompletionDataObject>
        {
            IsSuccessful = response.IsSuccessStatusCode,
            StatusCode = (int)response.StatusCode,
            Data = response.IsSuccessStatusCode ? await response.Content.ReadFromJsonAsync<ChatCompletionDataObject>(cancellationToken: cancellationToken) : default,
            RawResult = await response.Content.ReadAsStreamAsync(cancellationToken: cancellationToken)
        };
    }

    public async Task<ApiResponse<CreateChatCompletionActionOutput>> CreateChatCompletion(CreateChatCompletionActionInput input, CancellationToken cancellationToken = default)
    {
        var content = JsonSerializer.Serialize(input);
        return await PostAsync<CreateChatCompletionActionOutput>("chat/completions", new StringContent(content, System.Text.Encoding.UTF8, "application/json"), cancellationToken);
    }

    public async Task<ApiResponse<DeleteChatCompletionActionOutput>> DeleteChatCompletion(string completionId, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient
            .DeleteAsync($"chat/completions/{completionId}", cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        return new ApiResponse<DeleteChatCompletionActionOutput>
        {
            IsSuccessful = response.IsSuccessStatusCode,
            StatusCode = (int)response.StatusCode,
            Data = response.IsSuccessStatusCode ? await response.Content.ReadFromJsonAsync<DeleteChatCompletionActionOutput>(cancellationToken: cancellationToken) : default,
            RawResult = await response.Content.ReadAsStreamAsync(cancellationToken: cancellationToken)
        };
    }

    public async Task<ApiResponse<UpdateChatCompletionActionOutput>> UpdateChatCompletion(string completionId, Dictionary<string, string> metadata, CancellationToken cancellationToken = default)
    {
        var content = new StringContent(
            JsonSerializer.Serialize(new { metadata }),
            System.Text.Encoding.UTF8,
            "application/json");

        var response = await _httpClient
            .PostAsync($"chat/completions/{completionId}", content, cancellationToken)
            .ConfigureAwait(false);

        return new ApiResponse<UpdateChatCompletionActionOutput>
        {
            IsSuccessful = response.IsSuccessStatusCode,
            StatusCode = (int)response.StatusCode,
            Data = response.IsSuccessStatusCode ? await response.Content.ReadFromJsonAsync<UpdateChatCompletionActionOutput>(cancellationToken: cancellationToken) : default,
            RawResult = response.IsSuccessStatusCode ? null : await response.Content.ReadAsStreamAsync(cancellationToken: cancellationToken)
        };
    }

    public async Task<ApiResponse<ChatCompletionListDataObject>> GetChatCompletionList(string? after = null, CancellationToken cancellationToken = default)
    {
        var url = "chat/completions";
        if (!string.IsNullOrEmpty(after))
        {
            url += $"?after={after}";
        }

        var response = await _httpClient
            .GetAsync(url, cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        return new ApiResponse<ChatCompletionListDataObject>
        {
            IsSuccessful = response.IsSuccessStatusCode,
            StatusCode = (int)response.StatusCode,
            Data = response.IsSuccessStatusCode ? await response.Content.ReadFromJsonAsync<ChatCompletionListDataObject>(cancellationToken: cancellationToken) : default,
            RawResult = await response.Content.ReadAsStreamAsync(cancellationToken: cancellationToken)
        };
    }

    public async Task<ApiResponse<ChatMessagesDataObject>> GetChatMessages(string? after = null, CancellationToken cancellationToken = default)
    {
        var url = "chat/completions/messages";
        if (!string.IsNullOrEmpty(after))
        {
            url += $"?after={Uri.EscapeDataString(after)}";
        }

        var response = await _httpClient.GetAsync(url, cancellationToken);
        return new ApiResponse<ChatMessagesDataObject>
        {
            IsSuccessful = response.IsSuccessStatusCode,
            StatusCode = (int)response.StatusCode,
            Data = response.IsSuccessStatusCode ? await response.Content.ReadFromJsonAsync<ChatMessagesDataObject>(cancellationToken: cancellationToken) : default,
            RawResult = response.IsSuccessStatusCode ? null : await response.Content.ReadAsStreamAsync(cancellationToken: cancellationToken)
        };
    }

    public async Task<ApiResponse<CreateEmbeddingsActionOutput>> CreateEmbeddings(CreateEmbeddingsActionInput input, CancellationToken cancellationToken)
    {
        var content = JsonSerializer.Serialize(input);
        var stringContent = new StringContent(content, System.Text.Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync("embeddings", stringContent, cancellationToken);
        return new ApiResponse<CreateEmbeddingsActionOutput>
        {
            IsSuccessful = response.IsSuccessStatusCode,
            StatusCode = (int)response.StatusCode,
            Data = response.IsSuccessStatusCode ? await response.Content.ReadFromJsonAsync<CreateEmbeddingsActionOutput>(cancellationToken: cancellationToken) : default,
            RawResult = response.IsSuccessStatusCode ? null : await response.Content.ReadAsStreamAsync(cancellationToken: cancellationToken)
        };
    }

    public async Task<ApiResponse<CreateModerationActionOutput>> CreateModeration(CreateModerationActionInput input, CancellationToken cancellationToken)
    {
        var json = JsonSerializer.Serialize(input);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync("moderations", content, cancellationToken).ConfigureAwait(false);
        return new ApiResponse<CreateModerationActionOutput>
        {
            IsSuccessful = response.IsSuccessStatusCode,
            StatusCode = (int)response.StatusCode,
            Data = response.IsSuccessStatusCode ? await response.Content.ReadFromJsonAsync<CreateModerationActionOutput>(cancellationToken: cancellationToken) : default,
            RawResult = await response.Content.ReadAsStreamAsync(cancellationToken: cancellationToken)
        };
    }

    public async Task<ApiResponse<BatchDataObject>> GetBatch(string batchId, CancellationToken cancellationToken)
    {
        var response = await _httpClient.GetAsync($"batches/{batchId}", cancellationToken).ConfigureAwait(false);
        var content = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

        return new ApiResponse<BatchDataObject>
        {
            IsSuccessful = response.IsSuccessStatusCode,
            StatusCode = (int)response.StatusCode,
            Data = response.IsSuccessStatusCode ? JsonSerializer.Deserialize<BatchDataObject>(content) : default
        };
    }

    private async Task<ApiResponse<T>> SendRequestAsync<T>(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var response = await _httpClient.SendAsync(request, cancellationToken).ConfigureAwait(false);
        var content = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

        return new ApiResponse<T>
        {
            IsSuccessful = response.IsSuccessStatusCode,
            StatusCode = (int)response.StatusCode,
            Data = response.IsSuccessStatusCode ? JsonSerializer.Deserialize<T>(content) : default,
            RawResult = await response.Content.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false)
        };
    }

    public async Task<ApiResponse<CreateBatchActionOutput>> CreateBatch(CreateBatchActionInput input, CancellationToken cancellationToken)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, "v1/batches")
        {
            Content = JsonContent.Create(input)
        };

        return await SendRequestAsync<CreateBatchActionOutput>(request, cancellationToken).ConfigureAwait(false);
    }

    public async Task<ApiResponse<CancelBatchActionOutput>> CancelBatch(string batchId, CancellationToken cancellationToken)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, $"v1/batches/{batchId}/cancel")
        {
            Content = new StringContent(string.Empty)
        };

        return await SendRequestAsync<CancelBatchActionOutput>(request, cancellationToken).ConfigureAwait(false);
    }

    public async Task<ApiResponse<BatchListResponse>> GetBatchList(string? after = null, CancellationToken cancellationToken = default)
    {
        var url = "v1/batches";
        if (!string.IsNullOrEmpty(after))
        {
            url += $"?after={Uri.EscapeDataString(after)}";
        }

        var response = await _httpClient.GetAsync(url, cancellationToken).ConfigureAwait(false);
        var content = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

        return new ApiResponse<BatchListResponse>
        {
            IsSuccessful = response.IsSuccessStatusCode,
            StatusCode = (int)response.StatusCode,
            Data = response.IsSuccessStatusCode ? JsonSerializer.Deserialize<BatchListResponse>(content) : default,
            RawResult = await response.Content.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false)
        };
    }
    public async Task<ApiResponse<FileDataObject>> GetFile(string fileId, CancellationToken cancellationToken)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"v1/files/{fileId}");
        return await SendRequestAsync<FileDataObject>(request, cancellationToken);
    }

    public async Task<ApiResponse<string>> GetFileContent(string fileId, CancellationToken cancellationToken)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"v1/files/{fileId}/content");
        return await SendRequestAsync<string>(request, cancellationToken).ConfigureAwait(false);
    }
    
    public async Task<ApiResponse<UploadFileActionOutput>> UploadFile(
        byte[] fileContent,
        string filename,
        string purpose,
        CancellationToken cancellationToken)
    {
        using var content = new MultipartFormDataContent();
        content.Add(new ByteArrayContent(fileContent), "file", filename);
        content.Add(new StringContent(purpose), "purpose");

        var request = new HttpRequestMessage(HttpMethod.Post, "v1/files")
        {
            Content = content
        };

        return await SendRequestAsync<UploadFileActionOutput>(request, cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<ApiResponse<DeleteFileActionOutput>> DeleteFile(string fileId, CancellationToken cancellationToken)
    {
        var request = new HttpRequestMessage(HttpMethod.Delete, $"v1/files/{fileId}");
        return await SendRequestAsync<DeleteFileActionOutput>(request, cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<ApiResponse<FileListResponse>> GetFileList(string? after = null, CancellationToken cancellationToken = default)
    {
        var url = "v1/files";
        if (!string.IsNullOrEmpty(after))
        {
            url += $"?after={Uri.EscapeDataString(after)}";
        }

        var request = new HttpRequestMessage(HttpMethod.Get, url);
        return await SendRequestAsync<FileListResponse>(request, cancellationToken).ConfigureAwait(false);
    }

    public async Task<ApiResponse<FineTuningCheckpointsResponse>> GetFineTuningCheckpoints(
        string fineTuningJobId,
        string? after = null,
        CancellationToken cancellationToken = default)
    {
        var url = $"v1/fine_tuning/jobs/{fineTuningJobId}/checkpoints";
        if (!string.IsNullOrEmpty(after))
        {
            url += $"?after={Uri.EscapeDataString(after)}";
        }

        var request = new HttpRequestMessage(HttpMethod.Get, url);
        return await SendRequestAsync<FineTuningCheckpointsResponse>(request, cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<ApiResponse<FineTuningEventsResponse>> GetFineTuningEvents(
        string fineTuningJobId,
        string? after = null,
        CancellationToken cancellationToken = default)
    {
        var url = $"v1/fine_tuning/jobs/{fineTuningJobId}/events";
        if (!string.IsNullOrEmpty(after))
        {
            url += $"?after={Uri.EscapeDataString(after)}";
        }

        var request = new HttpRequestMessage(HttpMethod.Get, url);
        return await SendRequestAsync<FineTuningEventsResponse>(request, cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<ApiResponse<FineTuningJobsResponse>> GetFineTuningJobs(
        string? after = null,
        CancellationToken cancellationToken = default)
    {
        var url = "v1/fine_tuning/jobs";
        if (!string.IsNullOrEmpty(after))
        {
            url += $"?after={Uri.EscapeDataString(after)}";
        }

        var request = new HttpRequestMessage(HttpMethod.Get, url);
        return await SendRequestAsync<FineTuningJobsResponse>(request, cancellationToken).ConfigureAwait(false);
    }

    public async Task<ApiResponse<FineTuningJobDataObject>> GetFineTuningJob(
        string fineTuningJobId,
        CancellationToken? cancellationToken = null)
    {
        var requestUrl = $"v1/fine_tuning/jobs/{fineTuningJobId}";
        var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);
        return await SendRequestAsync<FineTuningJobDataObject>(request, cancellationToken ?? CancellationToken.None).ConfigureAwait(false);
    }

    public async Task<ApiResponse<CreateFineTuningJobActionOutput>> CreateFineTuningJob(
        CreateFineTuningJobActionInput input,
        CancellationToken cancellationToken = default)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, "v1/fine_tuning/jobs")
        {
            Content = JsonContent.Create(input)
        };

        return await SendRequestAsync<CreateFineTuningJobActionOutput>(request, cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<ApiResponse<CancelFineTuningJobActionOutput>> CancelFineTuningJob(
        string fineTuningJobId,
        CancellationToken cancellationToken = default)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, $"v1/fine_tuning/jobs/{fineTuningJobId}/cancel")
        {
            Content = new StringContent(string.Empty)
        };

        return await SendRequestAsync<CancelFineTuningJobActionOutput>(request, cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<ApiResponse<CreateImageActionOutput>> CreateImage(
        CreateImageActionInput input,
        CancellationToken? cancellationToken = null)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, "v1/images/generations")
        {
            Content = JsonContent.Create(input)
        };

        return await SendRequestAsync<CreateImageActionOutput>(request, cancellationToken ?? CancellationToken.None).ConfigureAwait(false);
    }

    public async Task<ApiResponse<CreateEditImageActionOutput>> CreateEditImage(
        CreateEditImageActionInput input,
        CancellationToken? cancellationToken = null)
    {
        using var formData = new MultipartFormDataContent();
        formData.Add(new ByteArrayContent(input.Image), "image", "image.png");
        formData.Add(new StringContent(input.Prompt), "prompt");
        
        if (input.Mask != null)
            formData.Add(new ByteArrayContent(input.Mask), "mask", "mask.png");
        
        if (!string.IsNullOrEmpty(input.Model))
            formData.Add(new StringContent(input.Model), "model");
        
        if (input.N.HasValue)
            formData.Add(new StringContent(input.N.Value.ToString()), "n");
        
        if (!string.IsNullOrEmpty(input.ResponseFormat))
            formData.Add(new StringContent(input.ResponseFormat), "response_format");
        
        if (!string.IsNullOrEmpty(input.Size))
            formData.Add(new StringContent(input.Size), "size");
        
        if (!string.IsNullOrEmpty(input.User))
            formData.Add(new StringContent(input.User), "user");

        var request = new HttpRequestMessage(HttpMethod.Post, "v1/images/edits")
        {
            Content = formData
        };

        return await SendRequestAsync<CreateEditImageActionOutput>(request, cancellationToken ?? CancellationToken.None)
            .ConfigureAwait(false);
    }

    public async Task<ApiResponse<CreateVariationImageActionOutput>> CreateVariationImage(
        CreateVariationImageActionInput input,
        CancellationToken cancellationToken = default)
    {
        using var content = new MultipartFormDataContent();
        content.Add(new ByteArrayContent(input.Image), "image", "image.png");

        if (!string.IsNullOrEmpty(input.Model))
            content.Add(new StringContent(input.Model), "model");
        if (input.N.HasValue)
            content.Add(new StringContent(input.N.Value.ToString()), "n");
        if (!string.IsNullOrEmpty(input.ResponseFormat))
            content.Add(new StringContent(input.ResponseFormat), "response_format");
        if (!string.IsNullOrEmpty(input.Size))
            content.Add(new StringContent(input.Size), "size");
        if (!string.IsNullOrEmpty(input.User))
            content.Add(new StringContent(input.User), "user");

        var request = new HttpRequestMessage(HttpMethod.Post, "v1/images/variations")
        {
            Content = content
        };

        return await SendRequestAsync<CreateVariationImageActionOutput>(request, cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<ApiResponse<ModelDataObject>> GetModel(
        string modelId,
        CancellationToken cancellationToken = default)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"v1/models/{modelId}");
        return await SendRequestAsync<ModelDataObject>(request, cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<ApiResponse<ModelListDataObject>> GetModelList(
        CancellationToken cancellationToken = default)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, "v1/models");
        return await SendRequestAsync<ModelListDataObject>(request, cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<ApiResponse<DeleteModelActionOutput>> DeleteModel(
        string modelId,
        CancellationToken cancellationToken = default)
    {
        var request = new HttpRequestMessage(HttpMethod.Delete, $"v1/models/{modelId}");
        return await SendRequestAsync<DeleteModelActionOutput>(request, cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<ApiResponse<CreateUploadActionOutput>> CreateUpload(
        CreateUploadActionInput input,
        CancellationToken cancellationToken = default)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, "v1/uploads")
        {
            Content = JsonContent.Create(input)
        };

        return await SendRequestAsync<CreateUploadActionOutput>(request, cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<ApiResponse<UploadDataObject>> GetUpload(
        string uploadId,
        CancellationToken cancellationToken = default)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"v1/uploads/{uploadId}");
        return await SendRequestAsync<UploadDataObject>(request, cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<ApiResponse<AddPartUploadActionOutput>> AddUploadPart(
        string uploadId,
        byte[] data,
        CancellationToken cancellationToken = default)
    {
        using var content = new MultipartFormDataContent();
        content.Add(new ByteArrayContent(data), "data");

        var request = new HttpRequestMessage(HttpMethod.Post, $"v1/uploads/{uploadId}/parts")
        {
            Content = content
        };

        return await SendRequestAsync<AddPartUploadActionOutput>(request, cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<ApiResponse<CancelUploadActionOutput>> CancelUpload(
        string uploadId,
        CancellationToken cancellationToken = default)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, $"v1/uploads/{uploadId}/cancel")
        {
            Content = new StringContent(string.Empty)
        };

        return await SendRequestAsync<CancelUploadActionOutput>(request, cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<ApiResponse<CompleteUploadActionOutput>> CompleteUpload(
        string uploadId,
        string[] partIds,
        string? md5 = null,
        CancellationToken cancellationToken = default)
    {
        var requestBody = new
        {
            part_ids = partIds,
            md5 = md5
        };

        var request = new HttpRequestMessage(HttpMethod.Post, $"v1/uploads/{uploadId}/complete")
        {
            Content = JsonContent.Create(requestBody)
        };

        return await SendRequestAsync<CompleteUploadActionOutput>(request, cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<ApiResponse<AssistantDataObject>> GetAssistant(
        string assistantId,
        CancellationToken cancellationToken = default)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"v1/assistants/{assistantId}");
        return await SendRequestAsync<AssistantDataObject>(request, cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<ApiResponse<CreateAssistantActionOutput>> CreateAssistant(
        CreateAssistantActionInput input,
        CancellationToken cancellationToken = default)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, "v1/assistants")
        {
            Content = JsonContent.Create(input)
        };
        return await SendRequestAsync<CreateAssistantActionOutput>(request, cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<ApiResponse<DeleteAssistantActionOutput>> DeleteAssistant(
        string assistantId,
        CancellationToken cancellationToken = default)
    {
        var request = new HttpRequestMessage(HttpMethod.Delete, $"v1/assistants/{assistantId}");
        return await SendRequestAsync<DeleteAssistantActionOutput>(request, cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<ApiResponse<ModifyAssistantActionOutput>> ModifyAssistant(
        ModifyAssistantActionInput input,
        CancellationToken cancellationToken = default)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, $"v1/assistants/{input.AssistantId}")
        {
            Content = JsonContent.Create(input)
        };
        return await SendRequestAsync<ModifyAssistantActionOutput>(request, cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<ApiResponse<ListAssistantsDataObject>> ListAssistants(
        string? after = null,
        string? before = null,
        int? limit = null,
        string? order = null,
        CancellationToken cancellationToken = default)
    {
        var queryParams = new List<string>();
        if (!string.IsNullOrEmpty(after)) queryParams.Add($"after={Uri.EscapeDataString(after)}");
        if (!string.IsNullOrEmpty(before)) queryParams.Add($"before={Uri.EscapeDataString(before)}");
        if (limit.HasValue) queryParams.Add($"limit={limit}");
        if (!string.IsNullOrEmpty(order)) queryParams.Add($"order={Uri.EscapeDataString(order)}");

        var queryString = queryParams.Any() ? "?" + string.Join("&", queryParams) : string.Empty;
        var request = new HttpRequestMessage(HttpMethod.Get, $"v1/assistants{queryString}");
        
        return await SendRequestAsync<ListAssistantsDataObject>(request, cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<ApiResponse<ThreadDataObject>> GetThread(
        string threadId,
        CancellationToken cancellationToken = default)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"v1/threads/{threadId}");
        return await SendRequestAsync<ThreadDataObject>(request, cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<ApiResponse<CreateThreadActionOutput>> CreateThread(CreateThreadActionInput input, CancellationToken cancellationToken = default)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, "v1/threads")
        {
            Content = JsonContent.Create(input)
        };
        
        return await SendRequestAsync<CreateThreadActionOutput>(request, cancellationToken);
    }

    public async Task<ApiResponse<DeleteThreadActionOutput>> DeleteThread(string threadId, CancellationToken cancellationToken = default)
    {
        var request = new HttpRequestMessage(HttpMethod.Delete, $"v1/threads/{threadId}");
        return await SendRequestAsync<DeleteThreadActionOutput>(request, cancellationToken);
    }

    public async Task<ApiResponse<ModifyThreadActionOutput>> ModifyThread(
        ModifyThreadActionInput input,
        CancellationToken cancellationToken = default)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, $"v1/threads/{input.ThreadId}")
        {
            Content = JsonContent.Create(new
            {
                metadata = input.Metadata,
                tool_resources = input.ToolResources
            })
        };

        return await SendRequestAsync<ModifyThreadActionOutput>(request, cancellationToken);
    }

    public async Task<ApiResponse<MessageDataObject>> GetMessage(
        string threadId,
        string messageId,
        CancellationToken cancellationToken = default)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"v1/threads/{threadId}/messages/{messageId}");
        return await SendRequestAsync<MessageDataObject>(request, cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<ApiResponse<CreateMessageActionOutput>> CreateMessage(
        CreateMessageActionInput input,
        CancellationToken cancellationToken = default)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, $"v1/threads/{input.ThreadId}/messages")
        {
            Content = JsonContent.Create(new
            {
                role = input.Role,
                content = input.Content,
                attachments = input.Attachments,
                metadata = input.Metadata
            })
        };

        return await SendRequestAsync<CreateMessageActionOutput>(request, cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<ApiResponse<DeleteMessageActionOutput>> DeleteMessage(
        string threadId,
        string messageId,
        CancellationToken cancellationToken = default)
    {
        var request = new HttpRequestMessage(HttpMethod.Delete, $"v1/threads/{threadId}/messages/{messageId}");
        return await SendRequestAsync<DeleteMessageActionOutput>(request, cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<ApiResponse<ModifyMessageActionOutput>> ModifyMessage(
        string threadId,
        string messageId,
        Dictionary<string, string>? metadata,
        CancellationToken cancellationToken = default)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, $"v1/threads/{threadId}/messages/{messageId}");
        var content = new Dictionary<string, object>();
        
        if (metadata != null)
        {
            content["metadata"] = metadata;
        }

        request.Content = new StringContent(
            JsonSerializer.Serialize(content),
            System.Text.Encoding.UTF8,
            "application/json");

        return await SendRequestAsync<ModifyMessageActionOutput>(request, cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<ApiResponse<ListMessagesDataObject>> ListMessages(
        string threadId,
        string? after = null,
        string? before = null,
        int? limit = null,
        string? order = null,
        string? runId = null,
        CancellationToken cancellationToken = default)
    {
        var queryParams = new List<string>();
        if (!string.IsNullOrEmpty(after)) queryParams.Add($"after={Uri.EscapeDataString(after)}");
        if (!string.IsNullOrEmpty(before)) queryParams.Add($"before={Uri.EscapeDataString(before)}");
        if (limit.HasValue) queryParams.Add($"limit={limit}");
        if (!string.IsNullOrEmpty(order)) queryParams.Add($"order={Uri.EscapeDataString(order)}");
        if (!string.IsNullOrEmpty(runId)) queryParams.Add($"run_id={Uri.EscapeDataString(runId)}");

        var queryString = queryParams.Any() ? "?" + string.Join("&", queryParams) : string.Empty;
        var request = new HttpRequestMessage(HttpMethod.Get, $"v1/threads/{threadId}/messages{queryString}");
        
        return await SendRequestAsync<ListMessagesDataObject>(request, cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<ApiResponse<RunDataObject>> GetRun(
        string threadId,
        string runId,
        CancellationToken cancellationToken = default)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"v1/threads/{threadId}/runs/{runId}");
        return await SendRequestAsync<RunDataObject>(request, cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<ApiResponse<CancelRunActionOutput>> CancelRun(
        string threadId,
        string runId,
        CancellationToken cancellationToken)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, $"v1/threads/{threadId}/runs/{runId}/cancel");
        return await SendRequestAsync<CancelRunActionOutput>(request, cancellationToken).ConfigureAwait(false);
    }

    public async Task<ApiResponse<CreateRunActionOutput>> CreateRun(
        CreateRunActionInput input,
        CancellationToken cancellationToken = default)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, $"v1/threads/{input.ThreadId}/runs")
        {
            Content = JsonContent.Create(input)
        };

        return await SendRequestAsync<CreateRunActionOutput>(request, cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<ApiResponse<CreateThreadRunActionOutput>> CreateThreadRun(CreateThreadRunActionInput input, CancellationToken cancellationToken)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, "v1/threads/runs")
        {
            Content = JsonContent.Create(input)
        };

        return await SendRequestAsync<CreateThreadRunActionOutput>(request, cancellationToken);
    }

    public async Task<ApiResponse<ModifyRunActionOutput>> ModifyRun(
        ModifyRunActionInput input,
        CancellationToken cancellationToken = default)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, $"v1/threads/{input.ThreadId}/runs/{input.RunId}")
        {
            Content = JsonContent.Create(new { metadata = input.Metadata })
        };

        return await SendRequestAsync<ModifyRunActionOutput>(request, cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<ApiResponse<ListRunsDataObject>> ListRuns(
        string threadId,
        string? after = null,
        string? before = null,
        int? limit = null,
        string? order = null,
        CancellationToken cancellationToken = default)
    {
        var queryParams = new List<string>();
        if (!string.IsNullOrEmpty(after)) queryParams.Add($"after={Uri.EscapeDataString(after)}");
        if (!string.IsNullOrEmpty(before)) queryParams.Add($"before={Uri.EscapeDataString(before)}");
        if (limit.HasValue) queryParams.Add($"limit={limit}");
        if (!string.IsNullOrEmpty(order)) queryParams.Add($"order={Uri.EscapeDataString(order)}");

        var queryString = queryParams.Any() ? "?" + string.Join("&", queryParams) : string.Empty;
        var request = new HttpRequestMessage(HttpMethod.Get, $"v1/threads/{threadId}/runs{queryString}");
        
        return await SendRequestAsync<ListRunsDataObject>(request, cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<ApiResponse<SubmitToolOutputsRunActionOutput>> SubmitToolOutputs(
        SubmitToolOutputsRunActionInput input,
        CancellationToken cancellationToken = default)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, $"v1/threads/{input.ThreadId}/runs/{input.RunId}/submit_tool_outputs")
        {
            Content = JsonContent.Create(new { tool_outputs = input.ToolOutputs, stream = input.Stream })
        };

        return await SendRequestAsync<SubmitToolOutputsRunActionOutput>(request, cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<ApiResponse<RunStepDataObject>> GetRunStep(
        string threadId,
        string runId,
        string stepId,
        CancellationToken cancellationToken = default)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"v1/threads/{threadId}/runs/{runId}/steps/{stepId}");
        return await SendRequestAsync<RunStepDataObject>(request, cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<ApiResponse<ListRunStepsDataObject>> ListRunSteps(
        string threadId,
        string runId,
        string? after = null,
        string? before = null,
        int? limit = null,
        string? order = null,
        string[]? include = null,
        CancellationToken cancellationToken = default)
    {
        var queryParams = new List<KeyValuePair<string, string>>();

        if (!string.IsNullOrEmpty(after))
        {
            queryParams.Add(new KeyValuePair<string, string>("after", after));
        }

        if (!string.IsNullOrEmpty(before))
        {
            queryParams.Add(new KeyValuePair<string, string>("before", before));
        }

        if (limit.HasValue)
        {
            queryParams.Add(new KeyValuePair<string, string>("limit", limit.Value.ToString()));
        }

        if (!string.IsNullOrEmpty(order))
        {
            queryParams.Add(new KeyValuePair<string, string>("order", order));
        }

        if (include != null && include.Length > 0)
        {
            foreach (var item in include)
            {
                queryParams.Add(new KeyValuePair<string, string>("include[]", item));
            }
        }

        var request = new HttpRequestMessage(HttpMethod.Get, $"v1/threads/{threadId}/runs/{runId}/steps");
        if (queryParams.Any())
        {
            request.RequestUri = new Uri($"{request.RequestUri}?{string.Join("&", queryParams.Select(p => $"{p.Key}={Uri.EscapeDataString(p.Value)}"))}");
        }

        return await SendRequestAsync<ListRunStepsDataObject>(request, cancellationToken).ConfigureAwait(false);
    }

    public async Task<ApiResponse<VectorStoreDataObject>> GetVectorStore(
        string vectorStoreId,
        CancellationToken cancellationToken = default)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"v1/vector_stores/{vectorStoreId}");
        return await SendRequestAsync<VectorStoreDataObject>(request, cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<ApiResponse<CreateVectorStoreActionOutput>> CreateVectorStore(
        CreateVectorStoreActionInput input,
        CancellationToken cancellationToken = default)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, "v1/vector_stores")
        {
            Content = JsonContent.Create(input)
        };

        return await SendRequestAsync<CreateVectorStoreActionOutput>(request, cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<ApiResponse<DeleteVectorStoreActionOutput>> DeleteVectorStore(
        string vectorStoreId,
        CancellationToken cancellationToken = default)
    {
        var request = new HttpRequestMessage(HttpMethod.Delete, $"v1/vector_stores/{vectorStoreId}");
        return await SendRequestAsync<DeleteVectorStoreActionOutput>(request, cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<ApiResponse<ModifyVectorStoreActionOutput>> ModifyVectorStore(
        ModifyVectorStoreActionInput input,
        CancellationToken cancellationToken = default)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, $"v1/vector_stores/{input.VectorStoreId}")
        {
            Content = JsonContent.Create(new
            {
                name = input.Name,
                metadata = input.Metadata,
                expires_after = input.ExpiresAfter
            })
        };

        return await SendRequestAsync<ModifyVectorStoreActionOutput>(request, cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<ApiResponse<ListVectorStoresDataObject>> ListVectorStores(
        string? after = null,
        string? before = null,
        int? limit = null,
        string? order = null,
        CancellationToken cancellationToken = default)
    {
        var queryParams = new List<string>();
        if (!string.IsNullOrEmpty(after)) queryParams.Add($"after={Uri.EscapeDataString(after)}");
        if (!string.IsNullOrEmpty(before)) queryParams.Add($"before={Uri.EscapeDataString(before)}");
        if (limit.HasValue) queryParams.Add($"limit={limit}");
        if (!string.IsNullOrEmpty(order)) queryParams.Add($"order={Uri.EscapeDataString(order)}");

        var queryString = queryParams.Any() ? "?" + string.Join("&", queryParams) : string.Empty;
        var request = new HttpRequestMessage(HttpMethod.Get, $"v1/vector_stores{queryString}");
        
        return await SendRequestAsync<ListVectorStoresDataObject>(request, cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<ApiResponse<VectorStoreFileDataObject>> GetVectorStoreFile(
        string vectorStoreId,
        string fileId,
        CancellationToken cancellationToken = default)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"v1/vector_stores/{vectorStoreId}/files/{fileId}");
        return await SendRequestAsync<VectorStoreFileDataObject>(request, cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<ApiResponse<CreateVectorStoreFileActionOutput>> CreateVectorStoreFile(
        string vectorStoreId,
        CreateVectorStoreFileActionInput input,
        CancellationToken cancellationToken = default)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, $"v1/vector_stores/{vectorStoreId}/files")
        {
            Content = JsonContent.Create(new
            {
                file_id = input.FileId,
                attributes = input.Attributes,
                chunking_strategy = input.ChunkingStrategy
            })
        };

        return await SendRequestAsync<CreateVectorStoreFileActionOutput>(request, cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<ApiResponse<DeleteVectorStoreFileActionOutput>> DeleteVectorStoreFile(
        string vectorStoreId,
        string fileId,
        CancellationToken cancellationToken = default)
    {
        var request = new HttpRequestMessage(HttpMethod.Delete, $"v1/vector_stores/{vectorStoreId}/files/{fileId}");
        return await SendRequestAsync<DeleteVectorStoreFileActionOutput>(request, cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<ApiResponse<FileContentVectorStoreFileActionOutput>> GetVectorStoreFileContent(
        string vectorStoreId,
        string fileId,
        CancellationToken cancellationToken = default)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"v1/vector_stores/{vectorStoreId}/files/{fileId}/content");
        return await SendRequestAsync<FileContentVectorStoreFileActionOutput>(request, cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<ApiResponse<ListVectorStoreFilesDataObject>> ListVectorStoreFiles(
        string vectorStoreId,
        string? after = null,
        string? before = null,
        int? limit = null,
        string? order = null,
        string? filter = null,
        CancellationToken cancellationToken = default)
    {
        var queryParams = new List<string>();
        if (!string.IsNullOrEmpty(after)) queryParams.Add($"after={Uri.EscapeDataString(after)}");
        if (!string.IsNullOrEmpty(before)) queryParams.Add($"before={Uri.EscapeDataString(before)}");
        if (limit.HasValue) queryParams.Add($"limit={limit}");
        if (!string.IsNullOrEmpty(order)) queryParams.Add($"order={Uri.EscapeDataString(order)}");
        if (!string.IsNullOrEmpty(filter)) queryParams.Add($"filter={Uri.EscapeDataString(filter)}");

        var queryString = queryParams.Any() ? "?" + string.Join("&", queryParams) : string.Empty;
        var request = new HttpRequestMessage(HttpMethod.Get, $"v1/vector_stores/{vectorStoreId}/files{queryString}");
        
        return await SendRequestAsync<ListVectorStoreFilesDataObject>(request, cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<ApiResponse<VectorStoreFileInBatchDataObject>> GetVectorStoreFileBatch(
        string vectorStoreId,
        string batchId,
        CancellationToken cancellationToken = default)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"v1/vector_stores/{vectorStoreId}/file_batches/{batchId}");
        return await SendRequestAsync<VectorStoreFileInBatchDataObject>(request, cancellationToken);
    }

    public async Task<ApiResponse<CancelVectorStoreFileInBatchActionOutput>> CancelVectorStoreFileBatch(
        string vectorStoreId,
        string batchId,
        CancellationToken cancellationToken = default)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, $"v1/vector_stores/{vectorStoreId}/file_batches/{batchId}/cancel");
        return await SendRequestAsync<CancelVectorStoreFileInBatchActionOutput>(request, cancellationToken).ConfigureAwait(false);
    }

    public async Task<ApiResponse<CreateVectorStoreFileInBatchActionOutput>> CreateVectorStoreFileBatch(
        string vectorStoreId,
        CreateVectorStoreFileInBatchActionInput input,
        CancellationToken cancellationToken = default)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, $"v1/vector_stores/{vectorStoreId}/file_batches")
        {
            Content = JsonContent.Create(new
            {
                file_ids = input.FileIds,
                attributes = input.Attributes,
                chunking_strategy = input.ChunkingStrategy
            })
        };

        return await SendRequestAsync<CreateVectorStoreFileInBatchActionOutput>(request, cancellationToken).ConfigureAwait(false);
    }

    public async Task<ApiResponse<ListVectorStoreFilesInBatchDataObject>> ListVectorStoreFilesInBatch(
        string vectorStoreId,
        string batchId,
        string? after = null,
        string? before = null,
        string? filter = null,
        int? limit = null,
        string? order = null,
        CancellationToken cancellationToken = default)
    {
        var queryParams = new List<string>();
        if (!string.IsNullOrEmpty(after)) queryParams.Add($"after={Uri.EscapeDataString(after)}");
        if (!string.IsNullOrEmpty(before)) queryParams.Add($"before={Uri.EscapeDataString(before)}");
        if (!string.IsNullOrEmpty(filter)) queryParams.Add($"filter={Uri.EscapeDataString(filter)}");
        if (limit.HasValue) queryParams.Add($"limit={limit}");
        if (!string.IsNullOrEmpty(order)) queryParams.Add($"order={Uri.EscapeDataString(order)}");

        var queryString = queryParams.Any() ? "?" + string.Join("&", queryParams) : string.Empty;
        var request = new HttpRequestMessage(HttpMethod.Get, $"v1/vector_stores/{vectorStoreId}/file_batches/{batchId}/files{queryString}");
        
        return await SendRequestAsync<ListVectorStoreFilesInBatchDataObject>(request, cancellationToken)
            .ConfigureAwait(false);
    }
}