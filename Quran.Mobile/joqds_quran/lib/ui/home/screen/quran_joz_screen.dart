import 'package:flutter/material.dart';

class QuranJozScreen extends StatelessWidget {
  const QuranJozScreen({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return ListView.builder(
      itemCount: 30,
      itemBuilder: (context, index) {
        return Card(
          child: TextButton(
              onPressed: () {},
              child: Text(
                "جزء ${index + 1}",
                style: Theme.of(context).textTheme.headline6,
              )),
        );
      },
    );
  }
}
