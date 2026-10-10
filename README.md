# YandexGPT клиент

Простой C#-клиент для работы с **YandexGPT** через официальный API Yandex Cloud.

---

## Описание

Консольное приложение на C#, которое отправляет текстовый запрос (промпт) в модель **YandexGPT Lite** и выводит полученный ответ в консоль. В основе — HTTP-запрос к endpoint'у `https://llm.api.cloud.yandex.net/foundationModels/v1/completion`.

---

## Требования

- **.NET SDK** (6.0 или новее)
- Аккаунт в **Yandex Cloud** с активированным сервисом YandexGPT
- **API-ключ** сервисного аккаунта
- **Folder ID** (идентификатор каталога)
- NuGet-пакет **Newtonsoft.Json**

Установка пакета:

```bash
dotnet add package Newtonsoft.Json
```

---

## 🔧 Настройка

Открой файл и найди в классе `YandexGptClient` две строки:

```csharp
private static readonly string MyToken = "Ваш токен";
private static readonly string MyFolderId = "Ваша директория";
```

Замени значения:

| Переменная   | Что вписать                                       |
|--------------|---------------------------------------------------|
| `MyToken`    | API-ключ сервисного аккаунта                      |
| `MyFolderId` | ID каталога (folder), в котором включён YandexGPT |

Затем в методе `TryToAsk()` укажи свой запрос:

```csharp
string answer = await gpt.AskYandexGpt("Ваш запрос пишется сюда");
```

---

## Запуск

```bash
dotnet run
```

В консоли появится ответ модели.

---

## 📝 Примечания

- Используется модель `yandexgpt-lite` — быстрая и экономичная. Для более качественных ответов замени на `yandexgpt` в поле `modelUri`.
- `temperature = 0.6` — баланс между креативностью и предсказуемостью.
- `maxTokens = 2000` — ограничение длины ответа.
- ⚠️ Не публикуй реальный API-ключ в открытых репозиториях — храни его в переменных окружения или `secrets.json`.
