## FindJob

### Информационная система для работы кадрового агенства.  

Выполнены все пункты ТЗ. При наличии недочетов сообщите - исправлю.  
Разработка велась в **VS2019**, ORM - **NHibernate**, для настройки маппинга - **Fluent NHibernate**, на мой взгляд проще и нагляднее, нежели xml файлы конфигурации. Для отображения таблиц с фильтрацией и сортировкой использовал компонент [**mvc-grid**](https://mvc-grid.azurewebsites.net/) (не стал изобретать велосипед :)  ). 

#### Учетные записи для проверки (логин : пароль):   

    admin1@rigmad.ru : admin1  (Администратор)  
    jobseeker1@rigmad.ru : jobseeker1  (Соискатель)  
    jobseeker2@rigmad.ru : jobseeker2  (Соискатель)  
    employee1@rigmad.ru : employee1  (Работодатель)  

Для создания этих учетных записей есть SQL скрипт **CreateUsers.sql**  
[Usecase диаграмма](https://github.com/rigmad/FindJob/blob/master/usecase.png)  
