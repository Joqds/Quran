import 'package:flutter/material.dart';
import 'package:persian_number_utility/persian_number_utility.dart';

class QuranRubScreen extends StatelessWidget {
  const QuranRubScreen({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return GridView.builder(
      gridDelegate: const SliverGridDelegateWithMaxCrossAxisExtent(
          maxCrossAxisExtent: 100),
      itemCount: 240,
      itemBuilder: (context, index) {
        return Card(
          child: TextButton(
            onPressed: () {},
            child: Center(
              child: Text(
                "ربع \n${(index + 1).toString().toPersianDigit()}",
                style: Theme.of(context).textTheme.headline6,
                textAlign: TextAlign.center,
              ),
            ),
          ),
        );
      },
    );
  }
}
