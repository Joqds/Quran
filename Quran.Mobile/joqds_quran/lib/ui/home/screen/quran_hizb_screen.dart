import 'package:flutter/material.dart';
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
            onPressed: () {},
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
}
