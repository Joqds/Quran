import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:joqds_quran/bloc/bloc.dart';

import 'screen/read_screen.dart';

class ReadPage extends Page {
  const ReadPage(this.surahId) : super(key: const ValueKey(ReadPage));
  final int surahId;

  @override
  Route createRoute(BuildContext context) {
    return MaterialPageRoute(
      settings: this,
      builder: (context) {
        return Directionality(
            child: BlocProvider(
              create: (context) => AyahBloc(UnAyahState(surahId))
                ..add(LoadAyahBySurahEvent(surahId)),
              child: const ReadScreen(),
            ),
            textDirection: TextDirection.rtl);
      },
    );
  }
}
