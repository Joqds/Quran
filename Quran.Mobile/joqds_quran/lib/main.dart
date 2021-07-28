import 'package:flutter/material.dart';
import 'package:joqds_quran/surah_list.dart';

void main() {
  runApp(const MainPage());
}

class MainPage extends StatelessWidget {
  const MainPage({Key? key}) : super(key: key);
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'نرم افزار قرآنی جغدز',
      theme: ThemeData(
        // Define the default brightness and colors.
        brightness: Brightness.dark,
        primaryColor: Colors.lightBlue[800],
        // Define the default font family.
        fontFamily: 'Georgia',
        // Define the default TextTheme. Use this to specify the default
        // text styling for headlines, titles, bodies of text, and more.
        textTheme: const TextTheme(
            // headline1: TextStyle(fontSize: 72.0, fontWeight: FontWeight.bold),
            // headline6: TextStyle(fontSize: 36.0, fontStyle: FontStyle.italic),
            // bodyText2: TextStyle(fontSize: 14.0, fontFamily: 'Hind'),
            ),
      ),
      home: Directionality(
        textDirection: TextDirection.rtl,
        child: Scaffold(
          appBar: AppBar(
              title: Center(
                  child: Text(
            'نرم افزار قرآنی جغدز',
            style: Theme.of(context).textTheme.headline5,
          ))),
          body: const SurahList(),
        ),
      ),
    );
  }
}
