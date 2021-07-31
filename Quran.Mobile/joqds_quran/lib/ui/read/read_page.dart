import 'package:flutter/material.dart';

import 'screen/read_screen.dart';

class ReadPage extends Page {
  const ReadPage() : super(key: const ValueKey(ReadPage));

  @override
  Route createRoute(BuildContext context) {
    return MaterialPageRoute(
      settings: this,
      builder: (context) {
        return const Directionality(
            child: ReadScreen(), textDirection: TextDirection.rtl);
      },
    );
  }
}
