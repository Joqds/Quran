import 'package:convex_bottom_bar/convex_bottom_bar.dart';
import 'package:fluent_appbar/fluent_appbar.dart';
import 'package:flutter/material.dart';
import 'package:joqds_quran/ui/home/nav_bar.dart';
import 'package:joqds_quran/ui/home/screens.dart';

class HomeScreen extends StatefulWidget {
  const HomeScreen({Key? key}) : super(key: key);

  @override
  State<HomeScreen> createState() => _HomeScreenState();
}

class _HomeScreenState extends State<HomeScreen> {
  HomeScreenType _current = HomeScreenType.quran;
  final ScrollController scrollController = ScrollController();
  @override
  Widget build(BuildContext context) {
    return Scaffold(
        bottomNavigationBar:
            QuranNavbar(onNavBarTap: onNavBarTap, initScreen: _current),
        body: _current.screen,
        appBar: AppBar(
          title: const Text("Joqds Quran"),
          centerTitle: true,
          automaticallyImplyLeading: true,
          primary: true,
        ));
  }

  onNavBarTap(HomeScreenType tab) {
    setState(() {
      _current = tab;
    });
  }
}
