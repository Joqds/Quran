import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
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
