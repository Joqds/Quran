import 'package:flutter/material.dart';
import 'package:joqds_quran/ui/home/home_screen.dart';

class HomePage extends Page {
  const HomePage() : super(key: const ValueKey(HomePage));

  @override
  Route createRoute(BuildContext context) {
    return MaterialPageRoute(
      settings: this,
      builder: (context) {
        return const HomeScreen();
      },
    );
  }
}
