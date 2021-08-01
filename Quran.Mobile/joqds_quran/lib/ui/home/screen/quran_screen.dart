import 'package:flutter/material.dart';
import 'package:joqds_quran/ui/home/screen/quran_page_screen.dart';
import 'package:joqds_quran/ui/home/screen/quran_rub_screen.dart';
import 'package:joqds_quran/ui/home/screen/quran_surah_screen.dart';

import 'quran_hizb_screen.dart';
import 'quran_joz_screen.dart';

class QuranScreen extends StatefulWidget {
  const QuranScreen({Key? key}) : super(key: key);

  @override
  State<QuranScreen> createState() => _QuranScreenState();
}

class _QuranScreenState extends State<QuranScreen> {
  @override
  Widget build(BuildContext context) {
    return DefaultTabController(
      length: 5,
      child: Scaffold(
        appBar: AppBar(
            title: const TabBar(
          tabs: [
            Tab(text: "سوره"),
            Tab(text: "جزء"),
            Tab(text: "حزب"),
            Tab(text: "ربع"),
            Tab(text: "صحفحه"),
          ],
          isScrollable: true,
        )),
        body: const TabBarView(
          children: [
            QuranSurahScreen(),
            QuranJozScreen(),
            QuranHizbScreen(),
            QuranRubScreen(),
            QuranPageScreen(),
          ],
        ),
      ),
    );
  }
}
