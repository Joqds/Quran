import 'package:convex_bottom_bar/convex_bottom_bar.dart';
import 'package:flutter/material.dart';
import 'package:joqds_quran/ui/home/screens.dart';

class QuranNavbar extends StatelessWidget {
  QuranNavbar({Key? key, required this.onNavBarTap, required this.initScreen})
      : super(key: key);

  final Function(HomeScreenType tab) onNavBarTap;
  final HomeScreenType initScreen;
  @override
  Widget build(BuildContext context) {
    return ConvexAppBar(
      items: HomeScreenType.values
          .map((e) => TabItem(icon: e.icon, title: e.title))
          .toList(),
      initialActiveIndex: initScreen.index, //optional, default as 0
      onTap: (index) {
        onNavBarTap(HomeScreenType.values[index]);
      },
    );
  }

  final items = <TabItem>[
    const TabItem(icon: Icons.home, title: "قرآن"),
    const TabItem(icon: Icons.search, title: "جستجو"),
    const TabItem(icon: Icons.app_registration, title: "مشارکت"),
    const TabItem(icon: Icons.info, title: "درباره ما"),
    const TabItem(icon: Icons.people, title: "حساب"),
  ];
}
