import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:joqds_quran/bloc/bloc.dart';
import 'package:persian_number_utility/persian_number_utility.dart';

class QuranHizbScreen extends StatelessWidget {
  const QuranHizbScreen({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return GridView.builder(
      gridDelegate: const SliverGridDelegateWithMaxCrossAxisExtent(
          maxCrossAxisExtent: 100),
      itemCount: 120,
      itemBuilder: (context, index) {
        return Card(
          child: TextButton(
            onPressed: () => goToRead(context, index + 1),
            child: Center(
              child: Text(
                "حزب \n${(index + 1).toString().toPersianDigit()}",
                style: Theme.of(context).textTheme.headline6,
                textAlign: TextAlign.center,
              ),
            ),
          ),
        );
      },
    );
  }

  goToRead(BuildContext context, int hizbId) {
    BlocProvider.of<NavBloc>(context)
        .add(GoRead(type: ReadViewType.hizb, id: hizbId, context: context));
  }
}
