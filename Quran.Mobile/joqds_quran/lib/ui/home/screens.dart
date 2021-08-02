import 'package:flutter/material.dart';

import 'screen/index.dart';

enum HomeScreenType { quran, search, enroll, about, profile }

extension HomeScreenTypeExtention on HomeScreenType {
  dynamic get icon {
    switch (this) {
      case HomeScreenType.quran:
        return Icons.home;
      case HomeScreenType.search:
        return Icons.search;
      case HomeScreenType.enroll:
        return Icons.app_registration;
      case HomeScreenType.about:
        return Icons.info;
      case HomeScreenType.profile:
        return Icons.people;
    }
  }

  String? get title {
    switch (this) {
      case HomeScreenType.quran:
        return "کریم قرآن";
      case HomeScreenType.search:
        return "جستجو";
      case HomeScreenType.enroll:
        return "مشارکت";
      case HomeScreenType.about:
        return "درباره ما";
      case HomeScreenType.profile:
        return "حساب";
    }
  }

  Widget? get screen {
    switch (this) {
      case HomeScreenType.quran:
        return const QuranScreen();
      case HomeScreenType.search:
        return const SearchScreen();
      case HomeScreenType.enroll:
        return const EnrollScreen();
      case HomeScreenType.about:
        return const AboutScreen();
      case HomeScreenType.profile:
        return const ProfileScreen();
    }
  }
}
