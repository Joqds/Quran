import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:joqds_quran/bloc/bloc.dart';
import 'package:persian_number_utility/persian_number_utility.dart';

class QuranJozScreen extends StatelessWidget {
  const QuranJozScreen({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return ListView.builder(
      itemCount: 30,
      itemBuilder: (context, index) {
        return Card(
          child: TextButton(
              onPressed: () => goToRead(context, index + 1),
              child: Text(
                "جزء ${(index + 1).toString().toPersianDigit()}",
                style: Theme.of(context).textTheme.headline6,
              )),
        );
      },
    );
  }

  goToRead(BuildContext context, int jozId) {
    BlocProvider.of<NavBloc>(context)
        .add(GoRead(type: ReadViewType.joz, id: jozId, context: context));
  }
}
