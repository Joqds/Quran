import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:google_fonts/google_fonts.dart';
import 'package:joqds_quran/bloc/nav/index.dart';
import 'package:joqds_quran/router/quran_router_delegate.dart';

class QUranApp extends StatefulWidget {
  const QUranApp({Key? key}) : super(key: key);

  @override
  State<QUranApp> createState() => _QUranAppState();
}

class _QUranAppState extends State<QUranApp> {
  final delegate = QuranRouterDelegate();

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: "Joqds Quran",
      theme: ThemeData(
          textTheme:
              GoogleFonts.scheherazadeTextTheme(Theme.of(context).textTheme)
                  .copyWith(
                      headline6: const TextStyle(fontSize: 24),
                      headline5: const TextStyle(
                          fontSize: 24, fontWeight: FontWeight.bold),
                      headline2: const TextStyle(
                          fontWeight: FontWeight.bold, fontSize: 40),
                      subtitle1: const TextStyle(fontSize: 18))),
      home: BlocProvider(
        create: (context) => NavBloc(),
        child: Router(
          routerDelegate: delegate,
          backButtonDispatcher: RootBackButtonDispatcher(),
        ),
      ),
    );
  }
}
