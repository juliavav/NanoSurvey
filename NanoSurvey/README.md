# NanoSurvey
Тестовое задание в Тибурон

## Запуск
Необходимо предустановить [Docker](https://www.docker.com/)
```sh
docker-compose build
docker-compose up
```

## Примеры запросов и ответов


### GET api/surveys/[idSurvey]/questions/[idQuestion]

Пример: http://localhost:8090/api/surveys/1/questions/1

Ответ:

    {
    "text": "Какой у вас телефон?",
    "answers": [
        "Айфон",
        "Андроид"
    ]
}


### POST api/surveys/[idSurvey]/questions/[idQuestion]

Пример: http://localhost:8090/api/surveys/1/questions/1

Запрос:
{
"id": 1
}

Ответ:
2
