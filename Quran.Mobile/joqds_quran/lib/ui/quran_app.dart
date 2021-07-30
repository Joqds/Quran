import 'package:flutter/material.dart';
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
      home: Router(
        routerDelegate: delegate,
        backButtonDispatcher: RootBackButtonDispatcher(),
      ),
    );
  }
}
