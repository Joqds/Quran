import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:joqds_quran/bloc/bloc.dart';
import 'package:joqds_quran/ui/home/screen/home_screen.dart';

class HomePage extends Page {
  const HomePage() : super(key: const ValueKey(HomePage));

  @override
  Route createRoute(BuildContext context) {
    return MaterialPageRoute(
      settings: this,
      builder: (context) {
        return Directionality(
            child: BlocProvider(
              create: (context) =>
                  SurahBloc(const UnSurahState())..add(LoadSurahEvent()),
              child: const HomeScreen(),
            ),
            textDirection: TextDirection.rtl);
      },
    );
  }
}
