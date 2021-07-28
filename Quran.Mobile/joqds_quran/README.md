native splash screen:

    1. edit flutter_native_splash.yaml
    2. flutter pub run flutter_native_splash:create

swagger generator update:

    1. copy https://quran.api.joqds.ir/swagger/v1/swagger.json to swaggers/quran.swagger
    2. flutter pub run build_runner build
    3. edit lib/api/quran.swagger.dart
        3.1 baseUrl: 'https://quran.api.joqds.ir'
    TODO: create script or build_runner to automate

flutter_launcher_icons:

    1. copy file of logo into assets/images/logo.png
    2. flutter pub get
    3. flutter pub run flutter_launcher_icons:main