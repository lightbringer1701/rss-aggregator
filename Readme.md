# VERSION
dotnet --version
8.0.200
# BUILD:
dotnet build -c Release --no-self-contained --runtime linux-x64

build docker image (предварительно создать релиз, т.к. в образе только runtime):
sudo docker image build . -t rss-test:1.0.0

# СУБД
В качестве СУБД использовал postgres:14.10 в контейнере (compose файл в проекте) на 54441 порту
Строку подключения можно задать в appsettings.json (в т.ч. порт)

# RSS-ленты, которые подключал. Обзятелен guid для новостей
https://www.vedomosti.ru/rss/news.xml
https://lenta.ru/rss
https://ria.ru/export/rss2/archive/index.xml 

Сделал дефолтный порт 5001 (HTTP), генерировать сертификаты и использовать HTTPS не стал
Swagger оставил рабочим и для релиза
В нем же можно посмотреть API. Полный CRUD делать не стал, т.к. не требовалось.

