native splash screen:
    for modify settings:
        1. edit flutter_native_splash.yaml
        2. flutter pub run flutter_native_splash:create

swagger generator update:
    1. copy https://quran.api.joqds.ir/swagger/v1/swagger.json to swaggers/quran.swagger
    2. flutter pub run build_runner build
    TODO: create script or build_runner to automate